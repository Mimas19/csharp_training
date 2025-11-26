using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace addressbook_web_tests;

public class AddingContactToGroupTests : AuthTestBase
{
    [Test]
    public void TestAddingContactToGroup()
    {
        // Получаем все группы
        List<GroupData> groups = GroupData.GetAll();
        if (groups.Count == 0)
        {
            GroupData newGroup = new GroupData("DefaultGroup");
            app.Groups.Create(newGroup);
            groups = GroupData.GetAll();
        }

        // Получаем все контакты
        List<ContactData> contacts = ContactData.GetAll();
        if (contacts.Count == 0)
        {
            ContactData newContact = new ContactData("John", "Doe", "1234567890", "john@example.com", "Some address");
            app.Contact.CreateContact(newContact);
            contacts = ContactData.GetAll();
        }

        GroupData targetGroup = null;
        ContactData targetContact = null;

        // Находим такую пару, где контакт не в группе
        foreach (var group in groups)
        {
            List<ContactData> contactsInGroup = group.GetContacts();
            targetContact = contacts.Except(contactsInGroup).FirstOrDefault();
            if (targetContact != null)
            {
                targetGroup = group;
                break;
            }
        }

        // Если такой пары нет, создаём новую группу и контакт для теста
        if (targetContact == null)
        {
            // Создаем новый контакт
            var newContact = new ContactData("New", "Contact", "1234567890", "new@example.com", "New address");
            app.Contact.CreateContact(newContact);

            // Обновляем контакты из базы и находим созданный
            contacts = ContactData.GetAll();
            targetContact = contacts.FirstOrDefault(c => c.Name == newContact.Name && c.LastName == newContact.LastName);
            Assert.IsNotNull(targetContact, "Не удалось получить созданный контакт из базы.");

            // Создаем новую группу
            var newGroup = new GroupData("AdditionalGroup");
            app.Groups.Create(newGroup);

            // Обновляем группы из базы и находим созданную
            groups = GroupData.GetAll();
            targetGroup = groups.FirstOrDefault(g => g.Name == newGroup.Name);
            Assert.IsNotNull(targetGroup, "Не удалось получить созданную группу из базы.");
        }

        // Получаем контакты в выбранной группе до добавления
        List<ContactData> oldList = targetGroup.GetContacts();

        // Добавляем контакт в группу
        app.Contact.AddContactToGroup(targetContact, targetGroup);

        // Получаем контакты в группе после добавления
        List<ContactData> newList = targetGroup.GetContacts();

        // Добавляем добавленный контакт в старый список для сравнения
        oldList.Add(targetContact);

        // Сортируем списки для корректного сравнения
        oldList.Sort();
        newList.Sort();

        // Проверяем, что контакт добавлен в группу
        Assert.AreEqual(oldList, newList);
    }
}

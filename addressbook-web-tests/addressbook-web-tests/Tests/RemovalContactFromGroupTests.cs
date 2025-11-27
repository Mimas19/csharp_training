using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace addressbook_web_tests;

public class RemovingContactFromGroupTests : AuthTestBase
{
    [Test]
    public void TestRemovingContactFromGroup()
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

        // Находим такую пару, где контакт уже есть в группе
        foreach (var group in groups)
        {
            List<ContactData> contactsInGroup = group.GetContacts();
            targetContact = contactsInGroup.FirstOrDefault();
            if (targetContact != null)
            {
                targetGroup = group;
                break;
            }
        }

        // Если такой пары нет, создаём группу и добавляем в неё контакт для теста
        if (targetContact == null)
        {
            // Создаем новый контакт
            var newContact = new ContactData("New", "Contact", "1234567890", "new@example.com", "New address");
            app.Contact.CreateContact(newContact);
            contacts = ContactData.GetAll();
            targetContact = contacts.FirstOrDefault(c => c.Name == newContact.Name && c.LastName == newContact.LastName);
            Assert.IsNotNull(targetContact, "Не удалось получить созданный контакт из базы.");

            // Создаем новую группу
            var newGroup = new GroupData("TestGroup");
            app.Groups.Create(newGroup);
            groups = GroupData.GetAll();
            targetGroup = groups.FirstOrDefault(g => g.Name == newGroup.Name);
            Assert.IsNotNull(targetGroup, "Не удалось получить созданную группу из базы.");

            // Добавляем контакт в группу
            app.Contact.AddContactToGroup(targetContact, targetGroup);
        }

        // Получаем контакты в выбранной группе до удаления
        List<ContactData> oldList = targetGroup.GetContacts();

        // Удаляем контакт из группы
        app.Contact.RemoveContactFromGroup(targetContact, targetGroup);

        // Получаем контакты в группе после удаления
        List<ContactData> newList = targetGroup.GetContacts();

        // Удаляем удаленный контакт из старого списка для сравнения
        oldList.RemoveAll(c => c.Id == targetContact.Id || 
                              (c.Name == targetContact.Name && c.LastName == targetContact.LastName));

        // Сортируем списки для корректного сравнения
        oldList.Sort();
        newList.Sort();

        // Проверяем, что контакт удален из группы
        Assert.AreEqual(oldList, newList);
    }
}

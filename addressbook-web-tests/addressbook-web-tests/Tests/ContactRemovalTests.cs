using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;


namespace addressbook_web_tests
{
    [TestFixture]
    public class ContactRemovalTests : AuthTestBase
    {
        [Test]
        public void DeletingContactTest()
        {
            // Проверяем, есть ли контакты
            if (app.Contact.GetContactCount() == 0)
            {
                // Если нет — создаём тестовый контакт
                app.Contact.CreateContact(new ContactData("Sara", "Mislimova", "+79614072727",
                    "Sara@example.com", "Rostov-on-Don"));
            }

            // Удаляем первый контакт
            // Получаем список контактов до удаления
            List<ContactData> oldContacts = app.Contact.GetContactList();

            // Удаляем первый контакт по индексу 0
            app.Contact.Remove(0);

            // Получаем новый список контактов после удаления
            List<ContactData> newContacts = app.Contact.GetContactList();

            // Удаляем из старого списка контакт с индексом 0, ожидая, что списки совпадут
            oldContacts.RemoveAt(0);

            // Сортируем списки для корректного сравнения (если реализовано в ContactData)
            oldContacts.Sort();
            newContacts.Sort();

            // Сравниваем списки
            Assert.AreEqual(oldContacts, newContacts);
        }
    }
    
}

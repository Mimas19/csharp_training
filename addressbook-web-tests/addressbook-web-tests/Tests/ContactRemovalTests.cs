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
            app.Contact.Remove(1);
        }
        
    }
}

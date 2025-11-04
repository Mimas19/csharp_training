using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace addressbook_web_tests
{
    [TestFixture]
    public class ContactModificationTests : AuthTestBase
    {
        [Test]
        public void ContactModificationTest()
        {
            if (app.Contact.GetContactCount() == 0)
            {
                app.Contact.CreateContact(new ContactData("Sara", "Mislimova", "+79614072727", 
                    "Sara@example.com", "Rostov-on-Don"));
            }
            
            ContactData newData = new ContactData("ModifySara", "Mislimova", "+79614072727", 
                "Modifmimas19@gmail.com", "Rostov-on-Don");

            List<ContactData> oldContacts = app.Contact.GetContactList();

            // Модифицируем контакт с индексом 0
            app.Contact.Modify(0, newData);
            app.Navigator.GoToAddressbookPage();

            List<ContactData> newContacts = app.Contact.GetContactList();

            // Обновляем данные в старом списке (замена по индексу)
            oldContacts[0] = newData;

            // Сортируем списки для корректного сравнения
            oldContacts.Sort();
            newContacts.Sort();

            // Проверяем равенство списков
            Assert.AreEqual(oldContacts, newContacts);
            
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace addressbook_web_tests
{
    [TestFixture]
    public class ContactModificationTests : ContactTestBase
    {
        [Test]
        public void ContactModificationTest()
        {
            if (app.Contact.GetContactCount() == 0)
            {
                app.Contact.CreateContact(new ContactData(
                    "Sara", "Mislimova", "", "+79614072727", "", "Sara@example.com", "Rostov-on-Don"
                ));
            }
            
            ContactData newData = new ContactData(
                contactName: "ModifySara",
                contactLastName: "Mislimova",
                homePhone: "",               // передайте корректные значения или пустые строки
                mobilePhone: "+79614072727",
                workPhone: "",
                email: "Modifmimas19@gmail.com",
                address: "Rostov-on-Don"
            );

            List<ContactData> oldContacts = app.Contact.GetContactList();

            // Модифицируем контакт с индексом 0
            app.Contact.Modify(0, newData);
            app.Navigator.GoToAddressbookPage();
            
            ContactData oldContact = oldContacts[0];
            
            Assert.AreEqual(oldContacts.Count, app.Contact.GetContactCount());

            List<ContactData> newContacts = app.Contact.GetContactList();

            // Обновляем данные в старом списке (замена по индексу)
            oldContacts[0] = newData;

            // Сортируем списки для корректного сравнения
            oldContacts.Sort();
            newContacts.Sort();

            // Проверяем равенство списков
            Assert.AreEqual(oldContacts, newContacts);

            foreach (ContactData contact in newContacts)
            {
                if (contact.Id == oldContact.Id)
                {
                    Assert.AreEqual(newData.Name, contact.Name);
                }
            }
        }
    }
}

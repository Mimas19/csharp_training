using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using static NUnit.Framework.Assert;

namespace addressbook_tests_autoit
{
    [TestFixture]
    public class ContactCreationTests : TestBase
    {
        [Test]
        public void ContactCreationTest()
        {
            List<ContactData> oldContacts = app.Contacts.GetContactsList();

            ContactData contact = new ContactData()
            {
                Firstname = "Sara",
                Lastname = "Mislimova"
            };

            app.Contacts.Create(contact);

            List<ContactData> newContacts = app.Contacts.GetContactsList();
            oldContacts.Add(contact);
            oldContacts.Sort();
            newContacts.Sort();

            AreEqual(oldContacts, newContacts);

        }
    }
}
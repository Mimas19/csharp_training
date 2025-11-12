using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using System.Collections.Generic;


namespace addressbook_web_tests 

{
    [TestFixture]
    public class ContactCreationTests : AuthTestBase
    {
        public static IEnumerable<ContactData> RandomContactDataProvider()
        {
            List<ContactData> contacts = new List<ContactData>();
            for (int i = 0; i < 5; i++)
            {
                contacts.Add(new ContactData(
                    GenerateRandomString(20),
                    GenerateRandomString(20),
                    GenerateRandomString(15),
                    GenerateRandomString(15),
                    GenerateRandomString(15),
                    GenerateRandomString(30),
                    GenerateRandomString(50)
                ));
            }
            return contacts;
        }
        
        
        [Test, TestCaseSource("RandomContactDataProvider")]
        public void UserCanLoginAndCreateContacts(ContactData contact)
        {
            app.Navigator.GoToAddressbookEdit();
            List<ContactData> oldContacts = app.Contact.GetContactList();

            app.Navigator.GoToAddressbookPage();
            app.Contact.CreateContact(contact);

            app.Navigator.OpenHomePage();
            Assert.AreEqual(oldContacts.Count + 1, app.Contact.GetContactCount());

            List<ContactData> newContacts = app.Contact.GetContactList();
            oldContacts.Add(contact);
            oldContacts.Sort();
            newContacts.Sort();

            Assert.AreEqual(oldContacts, newContacts);
        }
        
        
        [Test]
        public void BadContactCreationTest()
        {
            app.Navigator.GoToAddressbookEdit();

            List<ContactData> oldContacts = app.Contact.GetContactList();

            ContactData contact = new ContactData("f'f", "", "", "", "");

            app.Contact.CreateContact(contact);
            
            Assert.AreEqual(oldContacts.Count +1, app.Contact.GetContactCount());

            List<ContactData> newContacts = app.Contact.GetContactList();

            oldContacts.Add(contact);
            oldContacts.Sort();
            newContacts.Sort();

            Assert.AreEqual(oldContacts, newContacts);
            
        }
    }
}
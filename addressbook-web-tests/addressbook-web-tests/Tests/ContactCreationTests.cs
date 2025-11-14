using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using System.Collections.Generic;
using System.IO;


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

        public static IEnumerable<ContactData> ContactDataFromFile()
        {
            List<ContactData> contacts = new List<ContactData>();
            string[] lines = File.ReadAllLines(@"contacts.csv");
            foreach (string l in lines)
            {
                string[] parts = l.Split(',');
                contacts.Add(new ContactData(parts[0])
                {
                    LastName = parts[1],
                    Address = parts[2],
                    Email = parts[3]

                });
            }
            return contacts;
        }
        
        [Test, TestCaseSource("ContactDataFromFile")]
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
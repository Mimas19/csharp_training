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
        [Test]
        public void UserCanLoginAndCreateContacts()
        {
            // создаём объект ContactData и задаём ему значения
            app.Navigator.GoToAddressbookEdit();
            
            List<ContactData> oldContacts = app.Contact.GetContactList();
            
            ContactData contact = new ContactData("Sara", "Mislimova", "+79614072727", 
                "mimas19@gmail.com", "Rostov-on-Don");

            app.Navigator.GoToAddressbookPage();
            app.Contact.CreateContact(contact);
            
            
            List<ContactData> newContacts = app.Contact.GetContactList(); 
            Assert.AreEqual(oldContacts.Count + 1, newContacts.Count);
            
        }
        
        [Test]
        public void EmptyContactCreationTest()
        {
            // тест на создание пустого контакта
            app.Navigator.GoToAddressbookEdit();
            
            List<ContactData> oldContacts = app.Contact.GetContactList();
            
            ContactData contact = new ContactData("", "", "", "", "");
            
            app.Navigator.GoToAddressbookPage();
            app.Contact.CreateContact(contact);
            
            List<ContactData> newContacts = app.Contact.GetContactList(); 
            Assert.AreEqual(oldContacts.Count + 1, newContacts.Count);
            
        }
        
        [Test]
        public void BadContactCreationTest()
        {
            app.Navigator.GoToAddressbookEdit();
            
            List<ContactData> oldContacts = app.Contact.GetContactList();
            ContactData contact = new ContactData("f'f", "", "", "", "");
            
            app.Contact.CreateContact(contact);
            
            List<ContactData> newContacts = app.Contact.GetContactList(); 
            Assert.AreEqual(oldContacts.Count + 1, newContacts.Count);
            
        }
    }
}
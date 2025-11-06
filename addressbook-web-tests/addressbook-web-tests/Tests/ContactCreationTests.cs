﻿using System;
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
            
            Assert.AreEqual(oldContacts.Count +1, app.Contact.GetContactCount());
            
            List<ContactData> newContacts = app.Contact.GetContactList(); 
            
            // Добавляем новый контакт в старый список
            oldContacts.Add(contact);

            // Сортируем для корректного сравнения
            oldContacts.Sort();
            newContacts.Sort();

            // Проверяем, что списки равны
            Assert.AreEqual(oldContacts, newContacts);
        }
        
        [Test]
        public void EmptyContactCreationTest()
        {
            app.Navigator.GoToAddressbookEdit();

            List<ContactData> oldContacts = app.Contact.GetContactList();

            ContactData contact = new ContactData("", "", "", "", "");
            
            app.Contact.CreateContact(contact);
            app.Navigator.GoToAddressbookPage();
            
            Assert.AreEqual(oldContacts.Count +1, app.Contact.GetContactCount());

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
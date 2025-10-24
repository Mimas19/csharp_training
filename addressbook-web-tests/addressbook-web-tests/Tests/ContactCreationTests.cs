using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;


namespace addressbook_web_tests 

{
    [TestFixture]
    public class ContactCreationTests : AuthTestBase
    {
        [Test]
        public void UserCanLoginAndCreateContacts()
        {
            // создаём объект ContactData и задаём ему значения
            ContactData contact = new ContactData("Sara", "Mislimova", "+79614072727", 
                "mimas19@gmail.com", "Rostov-on-Don");

            app.Navigator.GoToAddressbookEdit();
            app.Contact.CreateContact(contact);
            
            app.Navigator.GoToAddressbookPage();
        }
        
        [Test]
        public void EmptyContactCreationTest()
        {
            // тест на создание пустого контакта
            ContactData contact = new ContactData("", "", "", "", "");
            
            app.Navigator.GoToAddressbookEdit();
            app.Contact.CreateContact(contact);
            app.Navigator.GoToAddressbookPage();
        }

    }
}

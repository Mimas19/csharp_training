using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;


namespace addressbook_web_tests 

{
    [TestFixture]
    public class ContactCreationTests : TestBase
    {
        [Test]
        public void UserCanLoginAndCreateContacts()
        {
            app.Navigator.GoToAddressbookEdit();
            app.Contact.CreateContact(contact);
            
            app.Navigator.GoToAddressbookPage();
        }
    }
}

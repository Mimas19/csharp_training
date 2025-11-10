using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace addressbook_web_tests 
{
    [TestFixture]
    public class ContactInformationTests : AuthTestBase
    {
        [Test]
        public void TestContactInformation()
        {
            ContactData fromTable = app.Contact.GetContactInformationFromTable(0);
            ContactData fromForm = app.Contact.GetContactInformationFromEditForm(0);
            
            //verification
            Assert.AreEqual(fromTable, fromForm);
            Assert.AreEqual(fromTable.Address, fromForm.Address);
            Assert.AreEqual(fromTable.AllPhones, fromForm.AllPhones);
            Assert.AreEqual(fromTable.Email, fromForm.Email); // Добавила сравнение мейлов
        }

        [Test]
        public void TestContactDetailsMatchesEditForm()
        {
            ContactData fromDetails = app.Contact.GetContactInformationFromDetailsPage(0);
            ContactData fromForm = app.Contact.GetContactInformationFromEditForm(0);
            
            //verification
            Assert.AreEqual(fromDetails, fromForm);
            Assert.AreEqual(fromDetails.Address, fromForm.Address);
            Assert.AreEqual(fromDetails.AllPhones, fromForm.AllPhones);
            Assert.AreEqual(fromDetails.Email, fromForm.Email);
            
        }
    }
}
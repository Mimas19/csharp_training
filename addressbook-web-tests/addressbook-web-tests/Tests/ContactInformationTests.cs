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
            Assert.AreEqual(fromTable.AllEmails, fromForm.AllEmails); // Добавила сравнение мейлов
        }

        [Test]
        public void TestContactDetailsMatchesEditForm()
        {
            // Получаем объект контакта из формы редактирования
            ContactData fromForm = app.Contact.GetContactInformationFromEditForm(0);
            // Генерируем строку "ожидаемый результат"
            string expectedDetails = ComposeContactDetailsString(fromForm);
            // Получаем строку с фактической страницы деталей
            string actualDetails = app.Contact.GetContactDetailsStringFromDetailsPage(0);

            Assert.AreEqual(expectedDetails, actualDetails);
        }

        private string ComposeContactDetailsString(ContactData contact)
        {
            StringBuilder result = new StringBuilder();

            // Имя и фамилия
            result.Append($"{contact.Name} {contact.LastName}\n");

            // Адрес (если есть)
            if (!string.IsNullOrWhiteSpace(contact.Address))
                result.Append(contact.Address + "\n");

            // Телефоны
            if (!string.IsNullOrWhiteSpace(contact.HomePhone))
                result.Append($"H: {contact.HomePhone}\n");
            if (!string.IsNullOrWhiteSpace(contact.MobilePhone))
                result.Append($"M: {contact.MobilePhone}\n");
            if (!string.IsNullOrWhiteSpace(contact.WorkPhone))
                result.Append($"W: {contact.WorkPhone}\n");

            // Email'ы
            if (!string.IsNullOrWhiteSpace(contact.Email))
                result.Append(contact.Email + "\n");
            if (!string.IsNullOrWhiteSpace(contact.Email1))
                result.Append(contact.Email1 + "\n");
            if (!string.IsNullOrWhiteSpace(contact.Email2))
                result.Append(contact.Email2 + "\n");

            return result.ToString().Trim();
        }
    }
}
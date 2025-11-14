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
            // Список всех блоков для итогового результата
            var blocks = new List<string>();

            // Блок: Имя и фамилия (одним блоком)
            string nameBlock = $"{contact.Name} {contact.LastName}".Trim();
            if (!string.IsNullOrEmpty(nameBlock))
                blocks.Add(nameBlock);

            // Блок: Адрес
            if (!string.IsNullOrWhiteSpace(contact.Address))
                blocks.Add(contact.Address.Trim());

            // Блок: Телефоны, объединенные в одну строку с переводами строк внутри блока
            var phoneLines = new List<string>();
            if (!string.IsNullOrWhiteSpace(contact.HomePhone))
                phoneLines.Add($"H: {contact.HomePhone.Trim()}");
            if (!string.IsNullOrWhiteSpace(contact.MobilePhone))
                phoneLines.Add($"M: {contact.MobilePhone.Trim()}");
            if (!string.IsNullOrWhiteSpace(contact.WorkPhone))
                phoneLines.Add($"W: {contact.WorkPhone.Trim()}");
            if (phoneLines.Count > 0)
                blocks.Add(string.Join("\n", phoneLines));

            // Блок: Email, объединенные в одну строку с переводами строк внутри блока
            var emailLines = new List<string>();
            if (!string.IsNullOrWhiteSpace(contact.Email))
                emailLines.Add(contact.Email.Trim());
            if (!string.IsNullOrWhiteSpace(contact.Email1))
                emailLines.Add(contact.Email1.Trim());
            if (!string.IsNullOrWhiteSpace(contact.Email2))
                emailLines.Add(contact.Email2.Trim());
            if (emailLines.Count > 0)
                blocks.Add(string.Join("\n", emailLines));

            // Итог: объединяем все непустые блоки двойными переводами строк
            return string.Join("\n\n", blocks);
        }

    }
}
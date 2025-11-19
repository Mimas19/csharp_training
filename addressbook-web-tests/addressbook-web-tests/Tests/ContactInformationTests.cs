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
            // Сравниваем имя и фамилию
            Assert.AreEqual(fromTable.Name, fromForm.Name);
            Assert.AreEqual(fromTable.LastName, fromForm.LastName);
    
            // Сравниваем адрес
            Assert.AreEqual(fromTable.Address, fromForm.Address);
    
            // Сравниваем телефоны
            Assert.AreEqual(fromTable.AllPhones, fromForm.AllPhones);
    
            // Сравниваем Email, если в таблице он не пустой
            if (!string.IsNullOrEmpty(fromTable.Email))
            {
                Assert.AreEqual(fromTable.Email, fromForm.Email);
            }

            // Сравниваем список email-ов
            Assert.AreEqual(fromTable.AllEmails, fromForm.AllEmails);
        }

        [Test]
        public void TestContactDetailsMatchesEditForm()
        {
            // Получаю объект контакта из формы редактирования
            ContactData fromForm = app.Contact.GetContactInformationFromEditForm(0);
            // Генерирую строку "ожидаемый результат"
            string expectedDetails = ComposeContactDetailsString(fromForm);
            // Получаю строку с фактической страницы деталей
            string actualDetails = app.Contact.GetContactDetailsStringFromDetailsPage(0);
            
            // Нормализую строки перед сравнением:
            // заменяю двойной перевод строки на одинарный и убираю лишние пробелы в конце
            string Normalize(string s) => s
                .Replace("\r\n", "\n")      // нормализую переводы строк к одному виду
                .Trim();                   // удаляю начальные и конечные пробелы и переводы строк


            expectedDetails = Normalize(expectedDetails);
            actualDetails = Normalize(actualDetails);

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

            // Итог: объединяю все непустые блоки двойными переводами строк
            return string.Join("\n\n", blocks);
        }

    }
}
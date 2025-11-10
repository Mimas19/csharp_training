using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using NUnit.Framework;
using OpenQA.Selenium.Support.UI;
using System;
using System.Text.RegularExpressions;
using System.Threading;
using OpenQA.Selenium;


namespace addressbook_web_tests;

public class ContactHelper : HelperBase
{
    public ContactHelper(ApplicationManager manager) 
        : base(manager)
    {
    }

    public ContactHelper CreateContact(ContactData contact)
    {
        manager.Navigator.OpenHomePage();
        
        InitNewContactCreation();
        FillContactForm(contact);
        SubmitContactCreation();
        return this;
    }
    
    public ContactHelper Modify(int i, ContactData newData)
    {
        manager.Navigator.OpenHomePage();
        FindAndSelectContact();
        InitContactModification();
        FillContactForm(newData);
        SubmitContactModification();
        return this;

    }

    public ContactHelper InitNewContactCreation()
    {
        _driver.FindElement(By.LinkText("add new")).Click();
        return this;
    }
        
    public ContactHelper FillContactForm(ContactData contact)
    {
        Type(By.Name("firstname"),contact.Name);
        Type(By.Name("lastname"),contact.LastName);
        Type(By.Name("address"),contact.Address);
        Type(By.Name("home"), contact.HomePhone);
        Type(By.Name("mobile"), contact.MobilePhone);
        Type(By.Name("work"), contact.WorkPhone);
        Type(By.Name("email"),contact.Email);
        return this;
    }
    
    public ContactHelper SubmitContactCreation()
    {
        _driver.FindElement(By.XPath("//div[@id='content']/form/input[20]")).Click();
        contactCache = null;
        return this;
    }

       
    public ContactHelper FindAndSelectContact()
    {
        _driver.FindElement(By.Name("selected[]")).Click();
        return this;
    }
        
    public ContactHelper DeleteContact()
    {
        _driver.FindElement(By.Name("delete")).Click();
        contactCache = null;
        return this;
    }

    public ContactHelper Remove(int i)
    {
        manager.Navigator.OpenHomePage();
        this.FindAndSelectContact();
        this.DeleteContact();
        return this;
    }
  
    public ContactHelper InitContactModification()
    {
        _driver.FindElement(By.XPath("//img[@src='icons/pencil.png' and @title='Edit']")).Click();
        return this;
    }
    
    public ContactHelper SubmitContactModification()
    {
        _driver.FindElement(By.Name("update")).Click();
        contactCache = null;
        return this;
    }
    
    public int GetContactCount()
    {
        manager.Navigator.OpenHomePage();
        return _driver.FindElements(By.Name("selected[]")).Count;
    }

    private List<ContactData> contactCache = null;

    public List<ContactData> GetContactList()
    {
        if (contactCache == null)
        {
            manager.Navigator.OpenHomePage();
            contactCache = new List<ContactData>();
             
            ICollection<IWebElement> rows = _driver.FindElements(By.Name("entry"));
            foreach (IWebElement row in rows)
            {
                var cells = row.FindElements(By.TagName("td"));
                string lastName = cells[1].Text;
                string firstName = cells[2].Text;
                string address = cells[3].Text;
                // Предполагается, что телефоны и email могут быть в других столбцах, например:
                string allPhones = cells[5].Text;  // или разбейте на отдельные телефоны, если нужно
                string email = cells[4].Text;

                
                contactCache.Add(new ContactData(firstName, lastName, "", "", "", email, address)
                {
                    Id = row.FindElement(By.TagName("input")).GetAttribute("value")
                });
                
            }
        }
        return new List<ContactData>(contactCache);
    }

    public ContactData GetContactInformationFromTable(int index)
    {
        manager.Navigator.OpenHomePage();
    
        IList<IWebElement> cells = _driver.FindElements(By.Name("entry"))[index]
            .FindElements(By.TagName("td"));

        string lastName = Normalize(cells[1].Text);
        string firstName = Normalize(cells[2].Text);
        string address = Normalize(cells[3].Text);
        string email = Normalize(cells[4].Text);
        string allPhones = NormalizePhones(cells[5].Text);

        return new ContactData(firstName, lastName)
        {
            Address = address,
            Email = email,
            AllPhones = allPhones
        };
    }


    public ContactData GetContactInformationFromEditForm(int index)
    {
        manager.Navigator.OpenHomePage();
        InitContactModification();
        
        string firstName = _driver.FindElement(By.Name("firstname")).GetAttribute("value");
        string lastName = _driver.FindElement(By.Name("lastname")).GetAttribute("value");
        string address = _driver.FindElement(By.Name("address")).GetAttribute("value");
        string homePhone = _driver.FindElement(By.Name("home")).GetAttribute("value");
        string mobilePhone = _driver.FindElement(By.Name("mobile")).GetAttribute("value");
        string workPhone = _driver.FindElement(By.Name("work")).GetAttribute("value");
        string email = _driver.FindElement(By.Name("email")).GetAttribute("value");
        
        return new ContactData(firstName, lastName, homePhone, mobilePhone, workPhone, email, address);
        
    }

    public int GetNumberOfSearchResults()
    {
       manager.Navigator.OpenHomePage();
       string text = _driver.FindElement(By.TagName("label")).Text;
       Match m = new Regex(@"\d+").Match(text);
       return Int32.Parse(m.Value);
    }

    public ContactData GetContactInformationFromDetailsPage(int index)
    {
        manager.Navigator.OpenHomePage();

        string contactId = GetContactIdByIndex(index);
        if (contactId == null)
            throw new ArgumentException("Invalid contact index");
        
        // Переходим на страницу деталей контакта
        _driver.Url = $"http://localhost/addressbook/view.php?id={contactId}";

        IWebElement content = _driver.FindElement(By.Id("content"));
        string contentText = content.Text;
        
        // Разделила по строкам для удобства парсинга 
        string[] lines = contentText
            .Split(new string[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries)
            // Пропускаем строки-сообщения об ошибках PHP - чтобы они не мешали распарсить реальные данные.
            // Это было сложно. не обошлось без помощи
            .Where(line => !line.StartsWith("Warning") && !line.StartsWith("mysqli_query") && !line.StartsWith("<b>Warning"))
            .ToArray();
        
        // Нахожу строку с именем и фамилией – обычно она первая, при этом должна содержать пробел и не начинаться с предупреждения
        string fullNameLine = lines.FirstOrDefault(line => line.Contains(" ")) ?? "";
        // Убираю слово "Modify", если оно есть перед именем (специфика страницы)
        fullNameLine = fullNameLine.Replace("Modify", "").Trim();
        
        // Разделяю имя и фамилию по пробелу
        string[] names = fullNameLine.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
        string firstName = names.Length > 0 ? names[0] : "";
        string lastName = names.Length > 1 ? names[1] : "";

        // Нахожу индекс строки с именем, чтобы от неё отсчитывать следующие строки
        int indexOfName = Array.IndexOf(lines, fullNameLine);
        string address = (indexOfName != -1 && indexOfName + 1 < lines.Length) ? lines[indexOfName + 1] : "";
        address = Normalize(address);

        var phonesLines = lines.Skip(indexOfName + 2)
            .Where(line => line.StartsWith("M:") || line.StartsWith("H:") || line.StartsWith("W:") || line.StartsWith("Mob:") || line.StartsWith("+") || line.StartsWith("P:"))
            .ToArray();
        string allPhones = string.Join("\n", phonesLines);
        allPhones = NormalizePhones(allPhones);
        
        // Ищу строку с email — строку, содержащую символ '@'
        string email = Normalize(lines.FirstOrDefault(line => line.Contains("@")) ?? "");

        // Возвращаю объект ContactData с собранными и нормализованными данными
        return new ContactData(firstName, lastName)
        {
            Address = address,
            AllPhones = allPhones,
            Email = email
        };
    }
    public string GetContactIdByIndex(int index)
    {
        manager.Navigator.OpenHomePage();
        var checkboxes = _driver.FindElements(By.CssSelector("input[type='checkbox'][name='selected[]']"));
        if (index >= 0 && index < checkboxes.Count)
        {
            return checkboxes[index].GetAttribute("id");
        }
        return null; // или выбросить исключение, если индекс неверный
    }
    
    private string Normalize(string value)
    {
        return value == null ? "" : value.Trim();
    }

    private string NormalizePhones(string phones)
    {
        if (string.IsNullOrEmpty(phones))
            return "";
        // Можно удалить лишние пробелы, пустые строки, настроить формат
        return phones.Trim();
    }


}
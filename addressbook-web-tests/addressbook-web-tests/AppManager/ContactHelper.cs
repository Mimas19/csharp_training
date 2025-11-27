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
        string lastName = cells[1].Text;
        string firstName = cells[2].Text;
        string address = cells[3].Text;
        string allEmail = cells[4].Text; // добавила чтение Email
        string allPhones = cells[5].Text;

        return new ContactData(firstName, lastName)
        {
            Address = address,
            AllEmails = allEmail,
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
        string email1 = _driver.FindElement(By.Name("email2")).GetAttribute("value");
        string email2 = _driver.FindElement(By.Name("email3")).GetAttribute("value");

        return new ContactData(firstName, lastName, homePhone, mobilePhone, workPhone, email, address)
        {
            Email1 = email1,
            Email2 = email2
        };
    }

    public int GetNumberOfSearchResults()
    {
       manager.Navigator.OpenHomePage();
       string text = _driver.FindElement(By.TagName("label")).Text;
       Match m = new Regex(@"\d+").Match(text);
       return Int32.Parse(m.Value);
    }

    public string GetContactDetailsStringFromDetailsPage(int index)
    {
        manager.Navigator.OpenHomePage();
        // Переход на страницу деталей контакта
        _driver.FindElements(By.XPath("//img[@title='Details']"))[index].Click();
        IWebElement content = _driver.FindElement(By.Id("content"));
        string contentText = content.Text;

        // Разбиваю строку на массив без удаления пустых строк
        string[] lines = contentText.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None);

        // Убираю только строки с предупреждениями, не убирая пустые строки
        var filtered = lines.Where(line => !line.Contains("Warning") 
                                           && !line.Contains("mysqli_query") && !string.IsNullOrWhiteSpace(line));

        // Сохраняю перевод строк, включая пустые (двойные) между блоками
        string clean = string.Join("\n\n", filtered);
        return clean;
    }

    public void AddContactToGroup(ContactData contact, GroupData group)
    {
        manager.Navigator.OpenHomePage();
        ClearGroupFilter();
        SelectContact(contact.Id);
        SelectGroupToAdd(group.Name);
        CommitAddingContactToGroup();
        new WebDriverWait(_driver, TimeSpan.FromSeconds(10))
            .Until(d => d.FindElements(By.CssSelector("div.msgbox")).Count > 0);
    }

    public void CommitAddingContactToGroup()
    {
        _driver.FindElement(By.Name("add")).Click();
    }

    public void SelectGroupToAdd(string name)
    {
        new SelectElement(_driver.FindElement(By.Name("to_group"))).SelectByText(name);
    }

    public void SelectContact(string contactId)
    {
        _driver.FindElement(By.Id(contactId)).Click();
    }

    public void ClearGroupFilter()
    {
        new SelectElement(_driver.FindElement(By.Name("group"))).SelectByText("[all]");
    }
    
    public void RemoveContactFromGroup(ContactData contact, GroupData group)
    {
        manager.Navigator.OpenHomePage();
        ClearGroupFilter();
        SelectContact(contact.Id);
        SelectContactInGroup(contact.Id);
        CommitRemovingContactFromGroup();
        new WebDriverWait(_driver, TimeSpan.FromSeconds(10))
            .Until(d => d.FindElements(By.CssSelector("div.msgbox")).Count > 0);
    }
    private void SelectContactInGroup(string contactId)
    {
        _driver.FindElement(By.CssSelector($"input[value='{contactId}']")).Click();
    }

    private void CommitRemovingContactFromGroup()
    {
        _driver.FindElement(By.Name("delete")).Click();
    }
    
}
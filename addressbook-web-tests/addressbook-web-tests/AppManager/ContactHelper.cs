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
        Type(By.Name("mobile"),contact.Phone);
        Type(By.Name("email"),contact.Email);
        return this;
    }
    
    public ContactHelper SubmitContactCreation()
    {
        _driver.FindElement(By.XPath("//div[@id='content']/form/input[20]")).Click();
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
        return this;
    }
    
    public int GetContactCount()
    {
        manager.Navigator.OpenHomePage();
        return _driver.FindElements(By.Name("selected[]")).Count;
    }

    public List<ContactData> GetContactList()
    {
        List<ContactData> contacts = new List<ContactData>();
        manager.Navigator.OpenHomePage(); // если завалится тест может не на эту страницу переход. проверить
        ICollection<IWebElement> rows = _driver.FindElements(By.Name("entry"));
        foreach (IWebElement row in rows)
        {
            var cells = row.FindElements(By.TagName("td"));
            string lastName = cells[1].Text;
            string firstName = cells[2].Text;

            contacts.Add(new ContactData(firstName, lastName, "", "", ""));
        }
        return contacts;
    }
}
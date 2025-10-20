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
        _driver.FindElement(By.Name("firstname")).Click();
        _driver.FindElement(By.Name("firstname")).Clear();
        _driver.FindElement(By.Name("firstname")).SendKeys(contact.Name);
        _driver.FindElement(By.Name("lastname")).Click();
        _driver.FindElement(By.Name("lastname")).Clear();
        _driver.FindElement(By.Name("lastname")).SendKeys(contact.LastName);
        _driver.FindElement(By.Name("address")).Click();
        _driver.FindElement(By.Name("address")).Clear();
        _driver.FindElement(By.Name("address")).SendKeys(contact.Address);
        _driver.FindElement(By.Name("mobile")).Click();
        _driver.FindElement(By.Name("mobile")).Clear();
        _driver.FindElement(By.Name("mobile")).SendKeys(contact.Phone);
        _driver.FindElement(By.Name("email")).Click();
        _driver.FindElement(By.Name("email")).Clear();
        _driver.FindElement(By.Name("email")).SendKeys(contact.Email);
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

}
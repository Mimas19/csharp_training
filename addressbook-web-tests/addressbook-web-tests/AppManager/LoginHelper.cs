using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using NUnit.Framework;
using OpenQA.Selenium.Support.UI;


namespace addressbook_web_tests;

public class LoginHelper : HelperBase
{
    public LoginHelper(ApplicationManager manager) : 
        base(manager)
    {
    }
    
    public void Login(AccountData account)
    {
        _driver.FindElement(By.Name("user")).Clear();
        _driver.FindElement(By.Name("user")).SendKeys(account.Username);
        _driver.FindElement(By.Name("pass")).Clear();
        _driver.FindElement(By.Name("pass")).SendKeys(account.Password);
        _driver.FindElement(By.XPath("//input[@value='Login']")).Click();
    }

}
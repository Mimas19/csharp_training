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
        Type(By.Name("user"), account.Username);
        Type(By.Name("pass"), account.Password);
        _driver.FindElement(By.XPath("//input[@value='Login']")).Click();
    }

}
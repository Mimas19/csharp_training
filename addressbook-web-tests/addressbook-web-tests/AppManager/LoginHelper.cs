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
        if (IsLoggedIn())
        {
            if (IsLoggedIn(account))
            {
                return;
            }

            Logout();
        }
        Type(By.Name("user"), account.Username);
        Type(By.Name("pass"), account.Password);
        _driver.FindElement(By.XPath("//input[@value='Login']")).Click();
    }

   public void Logout()
    {
        _driver.FindElement(By.LinkText("Logout")).Click();
    }
    
    public bool IsLoggedIn()
    {
        return IsElementPresent(By.Name("Logout"));
    }
   public bool IsLoggedIn(AccountData account)
    {
        return IsLoggedIn()
            && _driver.FindElement(By.Name("Logout")).FindElement(By.TagName("b")).Text
            == "(" + account.Username + ")";
    }
    
}
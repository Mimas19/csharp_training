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
        if (IsLoggedIn())
        {
            _driver.FindElement(By.LinkText("Logout")).Click();
        }
    }
    public bool IsLoggedIn()
    {
        return IsElementPresent(By.Name("logout"));
    }

    public bool IsLoggedIn(AccountData account)
    {
        return IsLoggedIn()
               && GetLoggetUserName() == account.Username;
    }

    public string GetLoggetUserName()
    {
        // Используем WebDriverWait, чтобы дождаться появления элемента
        WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
        IWebElement logoutElement = wait.Until(driver => driver.FindElement(By.Name("logout")));

        // Ищем тег <b> внутри элемента logout
        IWebElement userElement = logoutElement.FindElement(By.TagName("b"));

        string text = userElement.Text;
        // Используем Substring с проверками на длину, чтобы не получить исключение
        if (text.Length >= 2)
        {
            return text.Substring(1, text.Length - 2);
        }
        else
        {
            return text;
        }
    }
}
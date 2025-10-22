using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using NUnit.Framework;
using OpenQA.Selenium.Support.UI;


namespace addressbook_web_tests;

[TestFixture]
public class LoginTests : TestBase
{
    [Test]
    public void LoginWithValidCredentials()
    {
        // подготовка
        app.Auth.Logout();
        // действие 
        AccountData account = new AccountData("admin", "secret");
        app.Auth.Login(account);
        // проверка
        Assert.IsTrue(app.Auth.IsLoggedIn(account));
    }
    
    [Test]
    public void LoginWithInvalidCredentials()
    {
        // подготовка
        app.Auth.Logout();
        // действие 
        AccountData account = new AccountData("admin", "123123");
        app.Auth.Login(account);
        // проверка
        Assert.IsFalse(app.Auth.IsLoggedIn(account));
    }
}
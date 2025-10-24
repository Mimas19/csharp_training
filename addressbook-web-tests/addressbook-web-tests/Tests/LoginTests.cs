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
        // prepare
        app.Auth.Logout();
        
        // action
        AccountData account = new AccountData("admin", "secret");
        app.Auth.Login(account);
        
        // verification
        Assert.IsTrue(app.Auth.IsLoggedIn(account));
    }
        
    [Test]
    public void LoginWithInvalidCredentials()
    {
        // prepare
        app.Auth.Logout();
        
        // action
        AccountData account = new AccountData("admin", "123123");
        app.Auth.Login(account);
        
        // verification
        Assert.IsFalse(app.Auth.IsLoggedIn(account));
    }
}
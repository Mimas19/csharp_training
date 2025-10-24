using System.Text;
using NUnit.Framework;
using OpenQA.Selenium;

namespace addressbook_web_tests;

public class AuthTestBase : TestBase
{
    [SetUp]
    public void SetupLogin()
    {
        app.Auth.Login(new AccountData("admin","secret"));
    }
}
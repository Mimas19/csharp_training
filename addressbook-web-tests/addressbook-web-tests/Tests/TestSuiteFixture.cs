using System;
using System.Text;
using NUnit.Framework;
using OpenQA.Selenium;
using System.Collections;


namespace addressbook_web_tests;

[SetUpFixture]
public class TestSuiteFixture
{
    public static ApplicationManager app;
    
    [OneTimeSetUp]
    public void InitApplicationManager()
    {
        ApplicationManager app = ApplicationManager.GetInstance();
        app.Navigator.OpenHomePage();
        app.Auth.Login(new AccountData("admin","secret"));
    }

}
using System.Text;
using NUnit.Framework;
using OpenQA.Selenium;

namespace addressbook_web_tests;

public class TestBase
{
    protected ApplicationManager app;

    
    [SetUp]
    public void SetupTest()
    {
        app = ApplicationManager.GetInstance();
    }
}
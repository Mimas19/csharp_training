using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using NUnit.Framework;
using OpenQA.Selenium.Support.UI;

namespace addressbook_web_tests;

public class TestBase
{
    protected IWebDriver _driver;
    private StringBuilder _verificationErrors;
    protected string _baseUrl;
    
    protected LoginHelper loginHelper;
    protected NavigationHelper navigator;
    protected GroupHelper groupHelper;
    
    [SetUp]
    public void SetupTest()
    {
        _driver = new ChromeDriver();
        _baseUrl = "http://localhost/addressbook";
        _verificationErrors = new StringBuilder();
        
        loginHelper = new LoginHelper(_driver);
        navigator = new NavigationHelper(_driver, _baseUrl);
        groupHelper = new GroupHelper(_driver);
    }

    [TearDown]
    public void TeardownTest()
    {
        try
        {
            _driver.Quit();
        }
        catch (Exception)
        {
            // Ignore errors if unable to close the browser
        }

        Assert.That(_verificationErrors.ToString(), Is.EqualTo(""));
    }
    
   
    protected void Logout()
    {
        _driver.FindElement(By.LinkText("Logout")).Click();
    }



}
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using NUnit.Framework;
using OpenQA.Selenium.Support.UI;

namespace addressbook_web_tests;

public class ApplicationManager
{
    protected IWebDriver _driver;
    protected string _baseUrl;
    
    protected LoginHelper loginHelper;
    protected NavigationHelper navigator;
    protected GroupHelper groupHelper;

    public ApplicationManager()
    {
        _driver = new ChromeDriver();
        _baseUrl = "http://localhost/addressbook/";
        loginHelper = new LoginHelper(this);
        navigator = new NavigationHelper(this, _baseUrl);
        groupHelper = new GroupHelper(this);
    }

    public IWebDriver Driver
    {
        get
        {
            return _driver;
        }
    }

    public void Stop()
    {
        try
        {
            _driver.Quit();
        }
        catch (Exception)
        {
            // Ignore errors if unable to close the browser
        }

    }

    public LoginHelper Auth
    {
        get
        {
            return loginHelper;
        }
    }

    public NavigationHelper Navigator
    {
        get
        {
            return navigator;
        }
    }

    public GroupHelper Groups
    {
        get
        {
            return groupHelper;
        }
    }
    
} 
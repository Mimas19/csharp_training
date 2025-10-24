using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using NUnit.Framework;
using OpenQA.Selenium.Support.UI;
using System.Threading;
using System.Threading.Tasks;

namespace addressbook_web_tests;

public class ApplicationManager
{
    protected IWebDriver _driver;
    protected string _baseUrl;
    
    protected LoginHelper loginHelper;
    protected NavigationHelper navigator;
    protected GroupHelper groupHelper;
    
    protected ContactHelper contactHelper;

    private static ThreadLocal<ApplicationManager> app = new ThreadLocal<ApplicationManager>();

    private ApplicationManager()
    {
        _driver = new ChromeDriver();
        _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
        _baseUrl = "http://localhost/addressbook/";
        loginHelper = new LoginHelper(this);
        navigator = new NavigationHelper(this, _baseUrl);
        groupHelper = new GroupHelper(this);
        contactHelper = new ContactHelper(this);
    }

    ~ApplicationManager()
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

    public static ApplicationManager GetInstance()
    {
        if (! app.IsValueCreated)
        {
            ApplicationManager newInstance = new ApplicationManager();
            newInstance.Navigator.OpenHomePage();
            app.Value = newInstance;
        }
        return app.Value;
    }
    
    public IWebDriver Driver
    {
        get
        {
            return _driver;
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
    
    public ContactHelper Contact
    {
        get
        {
            return contactHelper;
        }
    }
} 
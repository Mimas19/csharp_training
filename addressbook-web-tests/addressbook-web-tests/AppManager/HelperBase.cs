using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using NUnit.Framework;
using OpenQA.Selenium.Support.UI;


namespace addressbook_web_tests;

public class HelperBase
{
    protected IWebDriver _driver;
    protected  ApplicationManager manager;

    public HelperBase(ApplicationManager manager)
    {
        this.manager = manager;
        this._driver = manager.Driver;
    }
}
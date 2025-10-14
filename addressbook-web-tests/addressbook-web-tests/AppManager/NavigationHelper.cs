using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using NUnit.Framework;
using OpenQA.Selenium.Support.UI;


namespace addressbook_web_tests;

public class NavigationHelper : HelperBase
{
    private readonly string baseURL;
    private string _baseURL;

    public NavigationHelper(ApplicationManager manager, string _baseURL) : base(manager)
    {
       this._baseURL = baseURL;
    }
    public void OpenHomePage()
    {
        _driver.Navigate().GoToUrl(_baseURL);
    }
    
    public void GoToGroupsPage()
    {
        _driver.FindElement(By.LinkText("groups")).Click();
    }
}
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using NUnit.Framework;
using OpenQA.Selenium.Support.UI;


namespace addressbook_web_tests;

public class NavigationHelper : HelperBase
{
    private string _baseURL;
    public NavigationHelper(ApplicationManager manager, string baseURL) : base(manager)
    {
        this._baseURL = baseURL;
    }
    public void OpenHomePage()
    {
        if (_driver.Url == _baseURL + "addressbook/group.php")
        {
            return;
        }
        _driver.Navigate().GoToUrl(_baseURL);
    }
    
    public void GoToGroupsPage()
    {
        if (_driver.Url == _baseURL + "addressbook/group.php"
            && IsElementPresent(By.Name("new")))
        {
            return;
        }
        _driver.FindElement(By.LinkText("groups")).Click();
    }
    
    public void GoToAddressbookEdit()
    {
        if (_driver.Url == _baseURL + "addressbook/edit.php"
            && IsElementPresent(By.CssSelector("#content > form > input[type='file']:nth-child(19)")))
        {
            return;
        }
        _driver.Navigate().GoToUrl("http://localhost/addressbook/edit.php");
    }

    public void GoToAddressbookPage()
    {
        if (_driver.Url == _baseURL + "addressbook/index.php"
            && IsElementPresent(By.CssSelector("#right > select > option:nth-child(1)")))
        {
            return;
        }
        _driver.Navigate().GoToUrl("http://localhost/addressbook/index.php");
    }

}
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
        if (_driver.Url == this._baseURL + "/addressbook/group.php"
            && IsElementPresent(By.XPath("//input[@title='Search for any text']")))
        {
            return;
        }
        _driver.Navigate().GoToUrl(_baseURL);
    }
    
    public void GoToGroupsPage()
    {
        if (_driver.Url == _baseURL + "/addressbook/group.php" 
            && IsElementPresent(By.Name("new")))
        {
            return;
        }
        _driver.FindElement(By.LinkText("groups")).Click();
    }
    
    public void GoToAddressbookEdit()
    {
        if (_driver.Url == _baseURL + "/addressbook/edit.php" 
            && IsElementPresent(By.Name("photo")))
        {
            return;
        }
        _driver.Navigate().GoToUrl("http://localhost/addressbook/edit.php");
    }

    public void GoToAddressbookPage()
    {
        _driver.Navigate().GoToUrl("http://localhost/addressbook/index.php");
    }

}
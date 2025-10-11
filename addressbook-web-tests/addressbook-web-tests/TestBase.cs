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
    
    [SetUp]
    public void SetupTest()
    {
        _driver = new ChromeDriver();
        _baseUrl = "http://localhost/addressbook";
        _verificationErrors = new StringBuilder();
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
    
    protected void OpenHomePage()
    {
        _driver.Navigate().GoToUrl(_baseUrl);
    }
    
    protected void Login(AccountData account)
    {
        _driver.FindElement(By.Name("user")).Clear();
        _driver.FindElement(By.Name("user")).SendKeys(account.Username);
        _driver.FindElement(By.Name("pass")).Clear();
        _driver.FindElement(By.Name("pass")).SendKeys(account.Password);
        _driver.FindElement(By.XPath("//input[@value='Login']")).Click();
    }
    
    protected void GoToGroupsPage()
    {
        _driver.FindElement(By.LinkText("groups")).Click();
    }
    
    protected void InitNewGroupCreation()
    {
        _driver.FindElement(By.Name("new")).Click();
    }
    
    protected void FillGroupForm(GroupData group)
    {
        _driver.FindElement(By.Name("group_name")).Click();
        _driver.FindElement(By.Name("group_name")).Clear();
        _driver.FindElement(By.Name("group_name")).SendKeys(group.Name);
        _driver.FindElement(By.Name("group_header")).Clear();
        _driver.FindElement(By.Name("group_header")).SendKeys(group.Header);
        _driver.FindElement(By.Name("group_footer")).Clear();
        _driver.FindElement(By.Name("group_footer")).SendKeys(group.Footer);
    }
    
    protected void SubmitGroupCreation()
    {
        _driver.FindElement(By.Name("submit")).Click();
    }

    protected void RemoveGroup()
    {
        _driver.FindElement(By.XPath("//input[@type='submit' and @name='delete' and @value='Delete group(s)']"))
            .Click();
    }
    
    protected void SelectGroup(int index)
    {
        _driver.FindElement(By.XPath("//span[" + index + "]/input")).Click();
    }


    protected void ReturnToGroupsPage()
    {
        _driver.FindElement(By.LinkText("group page")).Click();
    }

    protected void Logout()
    {
        _driver.FindElement(By.LinkText("Logout")).Click();
    }



}
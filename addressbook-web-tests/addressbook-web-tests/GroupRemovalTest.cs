using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace addressbook_web_tests
{
    [TestFixture]
    public class GroupRemovalTests
    {
        private IWebDriver _driver;
        private StringBuilder _verificationErrors;
        private string _baseUrl;
        private bool _acceptNextAlert = true;
        
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
        
        [Test]
        public void GroupRemovalTest()
        {
            OpenHomePage();
            Login(new AccountData("admin", "secret"));
            GoToGroupsPage();
            SelectAndDeleteGroups();
            ReturnToGroupsPage();
        }

        private void ReturnToGroupsPage()
        {
            _driver.FindElement(By.LinkText("group page")).Click();
        }

        private void SelectAndDeleteGroups()
        {
            _driver.FindElement(By.XPath("//input[@name='selected[]'])[3]")).Click();
            
        }

        private void GoToGroupsPage()
        {
            _driver.FindElement(By.LinkText("groups")).Click();
            _driver.Navigate().GoToUrl("http://localhost/addressbook/group.php");
        }

        private void Login(AccountData account)
        {
            _driver.FindElement(By.Name("user")).Clear();
            _driver.FindElement(By.Name("user")).SendKeys(account.Username);
            _driver.FindElement(By.Name("pass")).Clear();
            _driver.FindElement(By.Name("pass")).SendKeys(account.Password);
            _driver.FindElement(By.XPath("//input[@value='Login']")).Click();
        }

        private void OpenHomePage()
        {
            _driver.Navigate().GoToUrl(_baseUrl);
        }

        private bool IsElementPresent(By by)
        {
            try
            {
                _driver.FindElement(by);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }
        
        private bool IsAlertPresent()
        {
            try
            {
                _driver.SwitchTo().Alert();
                return true;
            }
            catch (NoAlertPresentException)
            {
                return false;
            }
        }
        
        private string CloseAlertAndGetItsText() {
            try {
                IAlert alert = _driver.SwitchTo().Alert();
                string alertText = alert.Text;
                if (_acceptNextAlert) {
                    alert.Accept();
                } else {
                    alert.Dismiss();
                }
                return alertText;
            } finally {
                _acceptNextAlert = true;
            }
        }
    }
}

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
    public class UntitledTestCase
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
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(3);
            
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
            Assert.AreEqual("", _verificationErrors.ToString());
        }
        
        [Test]
        public void TheUntitledTestCaseTest()
        {
            OpenHomePage();
            Login(new AccountData("admin", "secret"));
            FindAndSelectContact();
            DeleteContact();
            GoToHomePage();
        }

        private void GoToHomePage()
        {
            _driver.FindElement(By.LinkText("home page")).Click();
        }

        private void DeleteContact()
        {
            _driver.FindElement(By.Name("delete")).Click();
        }

        private void FindAndSelectContact()
        {
            var checkboxes = _driver.FindElements(By.Name("selected[]"));
            foreach (var checkbox in checkboxes)
            {
                if (!checkbox.Selected)
                    checkbox.Click();
            }
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
            _driver.Navigate().GoToUrl("http://localhost/addressbook");
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

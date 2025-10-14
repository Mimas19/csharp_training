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
    public class ContactCreationTests
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
        public void UserCanLoginAndCreateContacts()
        {
            OpenHomePage();
            Login(new AccountData("admin", "secret"));
            InitNewContactCreation();
            GoToAddressbookEdit();
            FillContactForm(new ContactData("Sara", "Mislimova", "+79614072727", 
                "mimas19@gmail.com", "Rostov on Don"));
            SubmitContactCreation();
            GoToHomePage();
            GoToAddressbookPage();
        }

        private void Logout()
        {
            _driver.FindElement(By.LinkText("Logout")).Click();
        }

        private void GoToAddressbookPage()
        {
            _driver.Navigate().GoToUrl("http://localhost/addressbook/index.php");
        }

        private void GoToAddressbookEdit()
        {
            _driver.Navigate().GoToUrl("http://localhost/addressbook/edit.php");
        }

        private void GoToHomePage()
        {
            _driver.FindElement(By.LinkText("home page")).Click();
        }

        private void SubmitContactCreation()
        {
            _driver.FindElement(By.XPath("//div[@id='content']/form/input[20]")).Click();
        }

        private void FillContactForm(ContactData contact)
        {
            _driver.FindElement(By.Name("firstname")).Click();
            _driver.FindElement(By.Name("firstname")).Clear();
            _driver.FindElement(By.Name("firstname")).SendKeys(contact.Name);
            _driver.FindElement(By.Name("lastname")).Click();
            _driver.FindElement(By.Name("lastname")).Clear();
            _driver.FindElement(By.Name("lastname")).SendKeys(contact.LastName);
            _driver.FindElement(By.Name("address")).Click();
            _driver.FindElement(By.Name("address")).Clear();
            _driver.FindElement(By.Name("address")).SendKeys(contact.Address);
            _driver.FindElement(By.Name("mobile")).Click();
            _driver.FindElement(By.Name("mobile")).Clear();
            _driver.FindElement(By.Name("mobile")).SendKeys(contact.Phone);
            _driver.FindElement(By.Name("email")).Click();
            _driver.FindElement(By.Name("email")).Clear();
            _driver.FindElement(By.Name("email")).SendKeys(contact.Email);
        }

        private void InitNewContactCreation()
        {
            _driver.FindElement(By.LinkText("add new")).Click();
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

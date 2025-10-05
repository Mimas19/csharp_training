using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace addressbook_web_tests
{
    [TestFixture]
    public class GroupCreationTests
    {
        private IWebDriver _driver;
        private StringBuilder _verificationErrors;
        private string _baseUrl;
        
        
        [SetUp]
        public void SetupTest()
        {
            // Использовала ChromeDriver вместо FirefoxDriver
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
        public void UserCanLoginAndCreateGroup()
        {
            _driver.Navigate().GoToUrl(_baseUrl);
            if (IsElementPresent(By.Name("user")))
            {
                _driver.FindElement(By.Name("user")).Clear();
                _driver.FindElement(By.Name("user")).SendKeys("Admin");
            }
            _driver.FindElement(By.Name("user")).Click();
            _driver.FindElement(By.Name("user")).Clear();
            _driver.FindElement(By.Name("user")).SendKeys("Admin");
            _driver.FindElement(By.Name("pass")).Clear();
            _driver.FindElement(By.Name("pass")).SendKeys("secret");
            _driver.FindElement(By.XPath("//input[@value='Login']")).Click();
            _driver.FindElement(By.LinkText("groups")).Click();
            _driver.FindElement(By.Name("new")).Click();
            _driver.FindElement(By.Name("group_name")).Click();
            _driver.FindElement(By.Name("group_name")).Clear();
            _driver.FindElement(By.Name("group_name")).SendKeys("Moon");
            _driver.FindElement(By.Name("group_header")).Clear();
            _driver.FindElement(By.Name("group_header")).SendKeys("header_3");
            _driver.FindElement(By.Name("group_footer")).Clear();
            _driver.FindElement(By.Name("group_footer")).SendKeys("footer_3");
            _driver.FindElement(By.Name("submit")).Click();
            _driver.FindElement(By.LinkText("group page")).Click();
            _driver.FindElement(By.LinkText("Logout")).Click();
        }
        
        // Добавила это, чтобы убрать предупреждения о неиспользуемых методах (нагуглила)
        #pragma warning disable CS0169
        #pragma warning disable CS0649
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
        
       
        
        
       
        #pragma warning restore CS0169
        #pragma warning restore CS0649
    }
}

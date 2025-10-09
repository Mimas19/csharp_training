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
            OpenHomePage();
            Login(new AccountData("admin","secret"));
            GoToGroupsPPage();
            InitNewGroupCreation();
            FillGroupForm(new GroupData("group_1", "header_name1", "footer_name1"));
            SubmitGroupCreation();
            ReturnToGroupsPage();
            Logout();
        }

        private void Logout()
        {
            _driver.FindElement(By.LinkText("Logout")).Click();
        }

        private void ReturnToGroupsPage()
        {
            _driver.FindElement(By.LinkText("group page")).Click();
        }

        private void SubmitGroupCreation()
        {
            _driver.FindElement(By.Name("submit")).Click();
        }

        private void FillGroupForm(GroupData group)
        {
            _driver.FindElement(By.Name("group_name")).Click();
            _driver.FindElement(By.Name("group_name")).Clear();
            _driver.FindElement(By.Name("group_name")).SendKeys(group.Name);
            _driver.FindElement(By.Name("group_header")).Clear();
            _driver.FindElement(By.Name("group_header")).SendKeys(group.Header);
            _driver.FindElement(By.Name("group_footer")).Clear();
            _driver.FindElement(By.Name("group_footer")).SendKeys(group.Footer);
        }

        private void InitNewGroupCreation()
        {
            _driver.FindElement(By.Name("new")).Click();
        }

        private void GoToGroupsPPage()
        {
            _driver.FindElement(By.LinkText("groups")).Click();
        }

        private void Login(AccountData account)
        {
            _driver.FindElement(By.Name("user")).Click();
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

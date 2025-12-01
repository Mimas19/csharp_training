using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using NUnit.Framework;
using OpenQA.Selenium.Support.UI;
using System.Threading;
using System.Threading.Tasks;

namespace mantis_tests

{
    public class ApplicationManager
    {
        protected IWebDriver _driver;
        protected string _baseUrl;
    
        private static ThreadLocal<ApplicationManager> app = new ThreadLocal<ApplicationManager>();

        private ApplicationManager()
        {
            _driver = new ChromeDriver();
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);
            _baseUrl = "http://localhost/mantisbt-2.26.3";
            Registration = new RegistrationHelper(this);
            Ftp = new FtpHelper(this);
            James = new JamesHelper(this);
            Mail = new MailHelper(this);     
            Project = new ProjectHelper(this);
            ManagementMenu = new ManagementMenuHelper(this, _baseUrl);
            Admin = new AdminHelper(this, _baseUrl);
            API = new APIHelper(this);

        }

        public AdminHelper Admin { get; set; }

        public ManagementMenuHelper ManagementMenu { get;  set; }
        public ProjectHelper Project { get; set; }

        public MailHelper Mail { get; set; }
        public APIHelper API { get; set;}

        public void Stop()
        {
            _driver?.Quit();
            _driver?.Dispose();
        }

        public JamesHelper James { get; set; }

        public FtpHelper Ftp { get; set; }

        public RegistrationHelper Registration { get; set; }

        ~ApplicationManager()
        {
            try
            {
                _driver.Quit();
            }
            catch (Exception)
            {
                // Ignore errors if unable to close the browser
            }
        }

        public static ApplicationManager GetInstance()
        {
            if (! app.IsValueCreated)
            {
                ApplicationManager newInstance = new ApplicationManager();
                newInstance._driver.Url = newInstance._baseUrl + "/login/page/php";
                app.Value = newInstance;
            }
            return app.Value;
        }
    
        public IWebDriver Driver
        {
            get
            {
                return _driver;
            }
        }
        
    } 
}



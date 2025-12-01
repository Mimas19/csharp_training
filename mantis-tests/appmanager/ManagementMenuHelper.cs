using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace mantis_tests
{
    public class ManagementMenuHelper : HelperBase
    {
        protected string baseURL;
        public ManagementMenuHelper(ApplicationManager manager, string baseURL)
            : base(manager)
        {
            this.baseURL = baseURL;
        }

        public void OpenSiteInformation()
        {
            if (driver.Url == baseURL + "mantisbt-2.22.1/manage_overview_page.php"
                && IsElementPresent(By.XPath("//th[@class='category' and contains(text(), 'Версия MantisBT')]")))
            {
                return;
            }
            driver.FindElement(By.XPath("//span[@class='menu-text' and contains(text(), ' Управление ')]")).Click();
        }

        internal void OpenManageProject()
        {
            if (driver.Url == baseURL + "mantisbt-2.22.1/manage_proj_page.php"
                && IsElementPresent(By.XPath("//button[@type='submit' and contains(text(), 'Создать новый проект')]")))
            {
                return;
            }
            driver.FindElements(By.CssSelector("ul.nav-tabs li"))[2].Click();
        }
    }
}
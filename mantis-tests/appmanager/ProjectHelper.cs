using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mantis_tests
{
    public class ProjectHelper : HelperBase
    {
        public ProjectHelper(ApplicationManager manager) : base(manager) { }

        public void AddProject(ProjectData project)
        {
            manager.ManagementMenu.OpenSiteInformation();
            manager.ManagementMenu.OpenManageProject();
            ClickOnCreateNewProjectButton();
            FillDataForProject(project);
            ClickAddProjectButton();
           
        }

        private void SubmitRemoval()
        {
            driver.FindElement(By.XPath("//input[@value='Удалить проект']")).Click();
        }

        public void Remove(int numberProject)
        {
            manager.ManagementMenu.OpenSiteInformation();
            manager.ManagementMenu.OpenManageProject();
            SelectProject(numberProject);
            DeleteProject();
            SubmitRemoval();
        }

        private void DeleteProject()
        {
            driver.FindElement(By.XPath("//input[@value='Удалить проект']")).Click();
        }

        private void SelectProject(int numberProject)
        {
            IWebElement cell = driver.FindElements(By.CssSelector("div.table-responsive"))[0].FindElement(By.TagName("tbody"));
            cell.FindElements(By.TagName("tr"))[numberProject].FindElements(By.TagName("td"))[0].FindElement(By.TagName("a")).Click();
        }

        internal void CheckProjects()
        {
            manager.ManagementMenu.OpenSiteInformation();
            manager.ManagementMenu.OpenManageProject();
            IWebElement cell = driver.FindElements(By.CssSelector("div.table-responsive"))[0].FindElement(By.TagName("tbody"));
            int count = cell.FindElements(By.TagName("tr")).Count();
            if (count == 0)
            {
                ProjectData project = new ProjectData("test") { };

                AddProject(project);
            }
        }

        private void ClickAddProjectButton()
        {
            driver.FindElement(By.XPath("//input[@value='Добавить проект']")).Click();
        }

        private void FillDataForProject(ProjectData project)
        {
            driver.FindElement(By.Name("name")).SendKeys(project.Name);            
        }

        private void ClickOnCreateNewProjectButton()
        {
            driver.FindElement(By.XPath("//button[@type='submit' and contains(text(), 'Создать новый проект')]")).Click();
        }    

     

        public int GetProjectCount()
        {
            manager.ManagementMenu.OpenSiteInformation();
            manager.ManagementMenu.OpenManageProject();
            IWebElement cell = driver.FindElements(By.CssSelector("div.table-responsive"))[0].FindElement(By.TagName("tbody"));
            return cell.FindElements(By.TagName("tr")).Count();
        }

        public List<ProjectData> GetProjectList()
        {
            List<ProjectData> projects = new List<ProjectData>();
            manager.ManagementMenu.OpenSiteInformation();
            manager.ManagementMenu.OpenManageProject();

            IWebElement cell = driver.FindElements(By.CssSelector("div.table-responsive"))[0].FindElement(By.TagName("tbody"));
            ICollection<IWebElement> elements = cell.FindElements(By.TagName("tr"));

            foreach (IWebElement element in elements)
            {
                IList<IWebElement> cells = element.FindElements(By.TagName("td"));
                string name = cells.ElementAt(0).Text;
                ProjectData project = new ProjectData(name);
                projects.Add(project);
            }
            return projects;
        }
        
        public void DeleteProject(string projectName)
        {
            manager.ManagementMenu.OpenSiteInformation();
            manager.ManagementMenu.OpenManageProject();

            IWebElement cell = driver.FindElements(By.CssSelector("div.table-responsive"))[0].FindElement(By.TagName("tbody"));
            var rows = cell.FindElements(By.TagName("tr"));

            int indexToRemove = -1;
            for (int i = 0; i < rows.Count; i++)
            {
                IList<IWebElement> cells = rows[i].FindElements(By.TagName("td"));
                string name = cells[0].Text;
                if (name == projectName)
                {
                    indexToRemove = i;
                    break;
                }
            }

            if (indexToRemove == -1)
            {
                throw new Exception($"Проект с именем '{projectName}' не найден.");
            }

            Remove(indexToRemove);
        }
    }
}
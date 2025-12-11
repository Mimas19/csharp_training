using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using NUnit.Framework;
using OpenQA.Selenium.Support.UI;

namespace mantis_tests
{
    [TestFixture]
    public class ProjectRemoving : TestBase
    {
        [SetUp]
        public void LoginBeforeTest()
        {
            AccountData admin = new AccountData("administrator", "root", "admin@localhost.localdomain");
            app.Login.Login(admin);
        }

        
        [Test]
        public void ProjectRemoveTest()
        {
            app.Project.CheckProjects(); 

            List<ProjectData> oldProjects = app.Project.GetProjectList();
            ProjectData toBeRemoved = oldProjects[0];

            app.Project.DeleteProject(toBeRemoved.Id);

            Assert.AreEqual(oldProjects.Count - 1, app.Project.GetProjectList().Count);

            List<ProjectData> newProjects = app.Project.GetProjectList();
            oldProjects.RemoveAt(0);
            oldProjects.Sort();
            newProjects.Sort();
            Assert.AreEqual(oldProjects, newProjects);
        }
    }
}

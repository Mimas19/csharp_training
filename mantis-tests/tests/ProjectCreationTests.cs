using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using NUnit.Framework;
using OpenQA.Selenium.Support.UI;

namespace mantis_tests
{
    public class ProjectCreation : TestBase
    {

        public static IEnumerable<ProjectData> RandomProjectDataProvider()
        {
            List<ProjectData> contacts = new List<ProjectData>();
            for (int i = 0; i < 1; i++)
            {
                contacts.Add(new ProjectData(GenerateRandomString(30))
                {
                });
            }

            return contacts;
        }

        [Test, TestCaseSource("RandomProjectDataProvider")]

        public void CreateProject(ProjectData project)
        {
            List<ProjectData> oldProjects = app.Project.GetProjectList();
            app.Project.AddProject(project);
            Assert.AreEqual(oldProjects.Count + 1, app.Project.GetProjectCount());

            List<ProjectData> newProjects = app.Project.GetProjectList();
            oldProjects.Add(project);
            oldProjects.Sort();
            newProjects.Sort();

            Assert.AreEqual(oldProjects, newProjects);
        }
    }
}
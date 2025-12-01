using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mantis_tests.Mantis;
using System.ServiceModel;


namespace mantis_tests
{
    public class APIHelper : HelperBase
    {
        public APIHelper(ApplicationManager manager) : base(manager) { }

        public List<ProjectData> GetProjectList(AccountData account)
        {
            var client = new MantisConnectPortTypeClient();
            mantis_tests.Mantis.ProjectData[] mantisList = client.mc_projects_get_user_accessible(account.Name, account.Password);

            var projectList = new List<ProjectData>();
            foreach (var p in mantisList)
            {
                projectList.Add(new ProjectData()
                {
                    Name = p.name,
                    Description = p.description,
                    Id = p.id
                });
            }
            return projectList;
        }

        public void CreateProjectForRemove(AccountData account, ProjectData project)
        {
            Mantis.MantisConnectPortTypeClient client = new Mantis.MantisConnectPortTypeClient();
            Mantis.ProjectData addedProject = new Mantis.ProjectData();
            addedProject.name = project.Name;
            addedProject.description = project.Description;
            client.mc_project_add(account.Name, account.Password, addedProject);
        }
        
        public void DeleteProject(AccountData account, string id)
        {
            Mantis.MantisConnectPortTypeClient client = new Mantis.MantisConnectPortTypeClient();
            client.mc_project_delete(account.Name, account.Password, id);
        }
        
        public void CreateNewIssue(AccountData account, ProjectData project, IssueData issueData)
        {
            Mantis.MantisConnectPortTypeClient client = new Mantis.MantisConnectPortTypeClient();
            Mantis.IssueData issue = new Mantis.IssueData();
            issue.summary = issueData.Summary;
            issue.description = issueData.Description;
            issue.category = issueData.Category;
            issue.project = new Mantis.ObjectRef();
            issue.project.id = project.Id;
            client.mc_issue_add(account.Name, account.Password, issue);
        }
    }
}
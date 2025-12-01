using System;
using System.ServiceModel;

namespace mantis_tests.Mantis
{
    [ServiceContract]
    
    public class ObjectRef
    {
        public string id { get; set; }
        public string name { get; set; }
    }

    public class IssueData
    {
        public string summary { get; set; }
        public string description { get; set; }
        public string category { get; set; }
        public ObjectRef project { get; set; }
    }
    public interface IMantisConnectPortType
    {
        [OperationContract]
        ProjectData[] mc_projects_get_user_accessible(string username, string password);

        [OperationContract]
        string mc_project_add(string username, string password, ProjectData project);

        [OperationContract]
        bool mc_project_delete(string username, string password, string project_id);
    }

    public class MantisConnectPortTypeClient : ClientBase<IMantisConnectPortType>, IMantisConnectPortType
    {
        public MantisConnectPortTypeClient()
            : base(new BasicHttpBinding(), new EndpointAddress("http://localhost/mantisbt-2.26.3/api/soap/mantisconnect.php"))
        {
        }

        public ProjectData[] mc_projects_get_user_accessible(string username, string password)
        {
            return Channel.mc_projects_get_user_accessible(username, password);
        }

        public string mc_project_add(string username, string password, ProjectData project)
        {
            return Channel.mc_project_add(username, password, project);
        }

        public bool mc_project_delete(string username, string password, string project_id)
        {
            return Channel.mc_project_delete(username, password, project_id);
        }
    }

    public class ProjectData
    {
        public string id { get; set; }          // строковый id
        public string name { get; set; }        // имя проекта
        public string description { get; set; } // описание
    }
    
    
}
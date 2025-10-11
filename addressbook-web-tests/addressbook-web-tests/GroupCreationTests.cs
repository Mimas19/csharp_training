using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace addressbook_web_tests
{
    [TestFixture]
    public class GroupCreationTests : TestBase
    {
        [Test]
        public void UserCanLoginAndCreateGroup()
        {
            OpenHomePage();
            Login(new AccountData("admin","secret"));
            GoToGroupsPage();
            InitNewGroupCreation();
            FillGroupForm(new GroupData("group_1", "header_name1", "footer_name1"));
            SubmitGroupCreation();
            ReturnToGroupsPage();
            Logout();
        }
    }
}

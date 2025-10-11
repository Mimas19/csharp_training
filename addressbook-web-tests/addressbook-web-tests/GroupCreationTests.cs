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
            navigator.OpenHomePage();
            loginHelper.Login(new AccountData("admin","secret"));
            navigator.GoToGroupsPage();
            groupHelper.InitNewGroupCreation();
            groupHelper.FillGroupForm(new GroupData("group_1", "header_name1", "footer_name1"));
            groupHelper.SubmitGroupCreation();
            groupHelper.ReturnToGroupsPage();
            Logout();
        }
    }
}

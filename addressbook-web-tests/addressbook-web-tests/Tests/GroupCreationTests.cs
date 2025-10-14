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
            GroupData group = new GroupData("group_1");
            group.Header = "header_name1";
            group.Footer = "footer_name1";
            
            app.Groups.Create(group);
        }
        
        [Test]
        public void EmptyGroupCreationTest()
        {
            GroupData group = new GroupData("");
            group.Header = "";
            group.Footer = "";
            
            app.Groups.Create(group);

        }
        
        
    }
}

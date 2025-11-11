using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Collections.Generic;

namespace addressbook_web_tests
{
    [TestFixture]
    public class GroupCreationTests : AuthTestBase
    {
        public static IEnumerable<GroupData> RandomGroupDataProvider()
        {
            List<GroupData> groups = new List<GroupData>();
            for (int i = 0; i < 5; i++)
            {
                groups.Add(new GroupData(GenerateRandomString(30))
                {
                    Header = GenerateRandomString(100),
                    Footer = GenerateRandomString(100),
                });
            }

            return groups;
        }
        

        [Test, TestCaseSource("RandomGroupDataProvider")]
        public void UserCanLoginAndCreateGroup(GroupData group)
        {
            List<GroupData> oldGroups = app.Groups.GetGroupList();
            
            app.Groups.Create(group);
            
            Assert.AreEqual(oldGroups.Count +1, app.Groups.GetGroupCount());

            List<GroupData> newGroups = app.Groups.GetGroupList();
            oldGroups.Add(group);
            oldGroups.Sort();
            newGroups.Sort();
            Assert.AreEqual(oldGroups, newGroups);
        }
        
        
        [Test]
        public void BadNameCreatingGroup()
        {
            GroupData group = new GroupData("f'f");
            group.Header = "";
            group.Footer = "";
            
            List<GroupData> oldGroups = app.Groups.GetGroupList();
            
            app.Groups.Create(group);
            
            Assert.AreEqual(oldGroups.Count +1, app.Groups.GetGroupCount());

            List<GroupData> newGroups = app.Groups.GetGroupList();
            oldGroups.Add(group);
            oldGroups.Sort();
            newGroups.Sort();
            Assert.AreEqual(oldGroups, newGroups);
        }
    }
}
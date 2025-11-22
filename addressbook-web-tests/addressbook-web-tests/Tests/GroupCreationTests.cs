using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Collections.Generic;
using System.IO;
using NUnit.Framework.Constraints;
using System.Xml;
using System.Xml.Serialization;
using System.Linq;

namespace addressbook_web_tests
{
    [TestFixture]
    public class GroupCreationTests : GroupTestBase
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

        public static IEnumerable<GroupData> GroupDataFromCsvFile()
        {
            List<GroupData> groups = new List<GroupData>();
            string[] lines = File.ReadAllLines(@"groups.csv");
            foreach (string l in lines)
            {
                string[] parts = l.Split(',');
                groups.Add(new GroupData(parts[0])
                {
                    Header = parts[1],
                    Footer = parts[2]
                }
                );

            }

            return groups;
        }
        
        public static IEnumerable<GroupData> GroupDataFromXmlFile()
        {
            return (List<GroupData>) 
                    new XmlSerializer(typeof(List<GroupData>))
                        .Deserialize(new StreamReader(@"groups.xml"));
        }
                
         
        [Test, TestCaseSource("GroupDataFromXmlFile")]
        public void UserCanLoginAndCreateGroup(GroupData group)
        {
            List<GroupData> oldGroups = GroupData.GetAll();
            
            app.Groups.Create(group);
            
            Assert.AreEqual(oldGroups.Count +1, app.Groups.GetGroupCount());

            List<GroupData> newGroups = GroupData.GetAll();
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

        [Test]
        public void TestDBConnectivity()
        {
            DateTime start = DateTime.Now;
            List<GroupData> fromUi = app.Groups.GetGroupList();
            DateTime end = DateTime.Now;
            System.Console.WriteLine("Took: " + end.Subtract(start));
            
            start = DateTime.Now;
            AddressBookDB db = new AddressBookDB();
            List<GroupData> fromDb = (from g in db.Groups select g).ToList();
            db.Close();
            end = DateTime.Now;
            Console.WriteLine("Took: " + end.Subtract(start));
        }
        
    }
}
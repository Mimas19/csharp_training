using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.Linq;
using System.IO;


namespace addressbook_web_tests
{
    [TestFixture]
    public class GroupRemovalTests : GroupTestBase

    {
        [Test]
        public void GroupRemovalTest()
        {
            // Проверяем, есть ли группы
            if (app.Groups.GetGroupCount() == 0)
            {
                // Если нет — создаём
                app.Groups.Create(new GroupData("Test group", "header", "footer"));
            }
            
            List<GroupData> oldGroups = GroupData.GetAll();
            GroupData toBeRemoved = oldGroups[0];
                
            app.Groups.Remove(toBeRemoved);
            
            Assert.AreEqual(oldGroups.Count - 1, app.Groups.GetGroupCount());
            
            List<GroupData> newGroups = GroupData.GetAll();
            
            oldGroups.RemoveAt(0);
            Assert.AreEqual(oldGroups, newGroups);

            foreach (GroupData group in newGroups)
            {
                Assert.AreNotEqual(group.Id, toBeRemoved.Id);
            }
        }
    }
}
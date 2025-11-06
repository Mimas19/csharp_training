using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using System.Collections.Generic;


namespace addressbook_web_tests
{
    [TestFixture]
    public class GroupRemovalTests : AuthTestBase

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
            
            List<GroupData> oldGroups = app.Groups.GetGroupList();
            
            app.Groups.Remove(0);
            
            Assert.AreEqual(oldGroups.Count - 1, app.Groups.GetGroupCount());
            
            List<GroupData> newGroups = app.Groups.GetGroupList();
            
            GroupData toBeRemoved = oldGroups[0];
            oldGroups.RemoveAt(0);
            Assert.AreEqual(oldGroups, newGroups);

            foreach (GroupData group in newGroups)
            {
                Assert.AreNotEqual(group.Id, toBeRemoved.Id);
            }
        }
    }
}
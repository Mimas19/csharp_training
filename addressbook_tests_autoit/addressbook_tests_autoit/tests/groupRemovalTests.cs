using System.Collections.Generic;
using NUnit.Framework;
using static NUnit.Framework.Assert;

namespace addressbook_tests_autoit
{
    [TestFixture]
    public class GroupRemovalTests : TestBase
    {
        [Test]
        public void TestGroupDeletion()
        {
            // Создаю тестовую группу, если её нет
            List<GroupData> oldGroups = app.Groups.GetGroupList();
            GroupData testGroup = new GroupData() { Name = "test" };
            bool exists = false;
    
            foreach (GroupData group in oldGroups)
            {
                if (group.Name == "test")
                {
                    exists = true;
                    break;
                }
            }
    
            if (!exists)
            {
                app.Groups.Add(testGroup);
            }

            // Получаю список до удаления
            List<GroupData> groupsBefore = app.Groups.GetGroupList();

            // Удаляю
            app.Groups.Remove(testGroup.Name);

            // Получаю список после удаления и проверяем
            List<GroupData> groupsAfter = app.Groups.GetGroupList();
    
            AreEqual(groupsBefore.Count - 1, groupsAfter.Count);
            CollectionAssert.DoesNotContain(groupsAfter, testGroup);
        }

    }
}
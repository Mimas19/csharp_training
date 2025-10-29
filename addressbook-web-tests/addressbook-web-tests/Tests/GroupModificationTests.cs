using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;



namespace addressbook_web_tests
{
    [TestFixture]
    public class GroupModificationTests : AuthTestBase
    {
        [Test]
        public void GroupModificationTest()
        {
            // Проверяем, есть ли группы
            if (app.Groups.GetGroupCount() == 0)
            {
                // Если групп нет, создаем тестовую
                app.Groups.Create(new GroupData("group_1")
                {
                    Header = "header_name1",
                    Footer = "footer_name1"
                });
            }

            // Данные для модификации
            GroupData newData = new GroupData("group_2")
            {
                Header = "header_name2",
                Footer = "footer_name2"
            };

            // Модифицируем первую группу
            
            List<GroupData> oldGroups = app.Groups.GetGroupList();

            app.Groups.Modify(0, newData);
            
            List<GroupData> newGroups = app.Groups.GetGroupList();
            oldGroups[0].Name = newData.Name;
            oldGroups.Sort();
            newGroups.Sort();
            Assert.AreEqual(oldGroups, newGroups);
        }
    }
}

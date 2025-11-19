using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace addressbook_web_tests
{
    [TestFixture]
    public class GroupModificationTests : GroupTestBase
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

            // Получаем список групп до модификации
            List<GroupData> oldGroups = GroupData.GetAll();
            GroupData toBeModified = oldGroups[0];
                
            app.Groups.Modify(toBeModified, newData);
            
            Assert.AreEqual(oldGroups.Count, app.Groups.GetGroupCount());
            
            // Получаем список групп после модификации
            List<GroupData> newGroups = GroupData.GetAll();
            
            // Обновляем данные в старом списке
            oldGroups[0].Name = newData.Name;
            oldGroups[0].Header = newData.Header;
            oldGroups[0].Footer = newData.Footer;
            
            oldGroups.Sort();
            newGroups.Sort();
            Assert.AreEqual(oldGroups, newGroups);

            // Проверяем, что у модифицированной группы изменились данные
            foreach (GroupData group in newGroups)
            {
                if (group.Id == toBeModified.Id)
                {
                    Assert.AreEqual(newData.Name, group.Name);
                    Assert.AreEqual(newData.Header, group.Header);
                    Assert.AreEqual(newData.Footer, group.Footer);
                }
            }
        }
    }
}
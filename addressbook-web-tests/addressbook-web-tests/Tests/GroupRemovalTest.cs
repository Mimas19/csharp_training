using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;


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

            // Удаляем первую группу
            app.Groups.Remove(1);
        }
    }
}

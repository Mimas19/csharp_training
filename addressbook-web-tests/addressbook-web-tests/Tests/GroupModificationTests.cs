using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;



namespace addressbook_web_tests
{
    [TestFixture]
    public class GroupModificationTests : TestBase
    {
        [Test]
        public void GroupModificationTest()
        {
            GroupData newData = new GroupData("group_2");
            newData.Header = "header_name2";
            newData.Footer = "footer_name2";

            app.Groups.Modify(1, newData);
        }
    }
}

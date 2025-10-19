using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;


namespace addressbook_web_tests
{
    [TestFixture]
    public class UntitledTestCase : TestBase
    {
        [Test]
        public void TheUntitledTestCaseTest()
        {
            app.Contact.Remove(1);
        }
    }
}

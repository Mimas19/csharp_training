using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace addressbook_web_tests
{
    [TestFixture]
    public class ContactModificationTests : AuthTestBase
    {
        [Test]
        public void ContactModificationTest()
        {
            ContactData newData = new ContactData("ModifySara", "Mislimova", "+79614072727", 
                "Modifmimas19@gmail.com", "Rostov-on-Don");
            
            app.Contact.Modify(1, newData);
            
        }
    }
}

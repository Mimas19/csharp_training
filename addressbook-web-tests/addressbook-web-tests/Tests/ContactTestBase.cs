using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace addressbook_web_tests;

public class ContactTestBase : AuthTestBase
{
    [TearDown]
    public void CompareContactsUI_DB()
    {
        if (PERFORM_LONG_UI_CHECKS)
        {
            List<ContactData> fromUI = app.Contact.GetContactList();
            List<ContactData> fromDB = ContactData.GetAll();
            fromUI.Sort();
            fromDB.Sort();
        
            Assert.AreEqual(fromDB, fromUI);
        }
        
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace addressbook_web_tests;

public class RemovingContactFromGroupTests : AuthTestBase
{
    
    [Test]
    public void RemovingContactFromGroupTest()
    {
        app.Groups.GroupExistenceCheck();
        app.Contact.ContactExistanceCheck();
        GroupData group = GroupData.GetAll()[0];
        app.Contact.ContactInGroupCheck(group);
        List<ContactData> oldList = group.GetContacts();
        ContactData contact = oldList[0];
        app.Contact.RemoveContactFromGroup(contact, group);
        List<ContactData> newList = group.GetContacts();
        oldList.Remove(contact);
        oldList.Sort();
        newList.Sort();
        Assert.AreEqual(oldList, newList);
    }
}

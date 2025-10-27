using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using NUnit.Framework;
using OpenQA.Selenium.Support.UI;

namespace addressbook_web_tests;

public class GroupHelper : HelperBase
{
   public GroupHelper(ApplicationManager manager) 
       : base(manager)
    {
    }

    public GroupHelper Create(GroupData group)
    {
        manager.Navigator.GoToGroupsPage();
        
        InitNewGroupCreation();
        FillGroupForm(group);
        SubmitGroupCreation();
        ReturnToGroupsPage();
        return this;
    }
    
    public GroupHelper Modify(int i, GroupData newData)
    {
        manager.Navigator.GoToGroupsPage();
        SelectGroup(i);
        InitGroupModification();
        FillGroupForm(newData);
        SubmitGroupModification();
        ReturnToGroupsPage();
        return this;
    }
  
    public GroupHelper Remove(int i)
    {
        manager.Navigator.GoToGroupsPage();
        SelectGroup(i);
        RemoveGroup();
        ReturnToGroupsPage();
        return this;
    }
   
    public GroupHelper InitNewGroupCreation()
    {
        _driver.FindElement(By.Name("new")).Click();
        return this;
    }
    
    
    public GroupHelper FillGroupForm(GroupData group)
    {
        Type(By.Name("group_name"),group.Name);
        Type(By.Name("group_header"),group.Header);
        Type(By.Name("group_footer"),group.Footer);
        return this;
    }
    

    public GroupHelper SubmitGroupCreation()
    {
        _driver.FindElement(By.Name("submit")).Click();
        return this;
    }

    public GroupHelper RemoveGroup()
    {
        _driver.FindElement(By.XPath("//input[@type='submit' and @name='delete' and @value='Delete group(s)']"))
            .Click();
        return this;
    }
    
    public GroupHelper SelectGroup(int index)
    {
        _driver.FindElement(By.XPath("//span[" + (index+1) + "]/input")).Click();
        return this;
    }


    public GroupHelper ReturnToGroupsPage()
    {
        _driver.FindElement(By.LinkText("group page")).Click();
        return this;
    }
    
    public GroupHelper SubmitGroupModification()
    {
        _driver.FindElement(By.Name("update")).Click();
        return this;
    }

    public GroupHelper InitGroupModification()
    {
        _driver.FindElement(By.Name("edit")).Click();
        return this;
    }

    public List<GroupData> GetGroupList()
    {
        List<GroupData> groups = new List<GroupData>();
        manager.Navigator.GoToGroupsPage();
        ICollection<IWebElement> elements = _driver.FindElements(By.CssSelector("span.group"));
        foreach (IWebElement element in elements)
        {
            groups.Add(new GroupData(element.Text));
        }
        return groups;
    }
}
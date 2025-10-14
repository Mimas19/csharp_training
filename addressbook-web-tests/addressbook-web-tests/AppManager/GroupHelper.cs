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
    
    public GroupHelper Remove(int i)
    {
        manager.Navigator.GoToGroupsPage();
        SelectGroup(1);
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
        _driver.FindElement(By.Name("group_name")).Click();
        _driver.FindElement(By.Name("group_name")).Clear();
        _driver.FindElement(By.Name("group_name")).SendKeys(group.Name);
        _driver.FindElement(By.Name("group_header")).Clear();
        _driver.FindElement(By.Name("group_header")).SendKeys(group.Header);
        _driver.FindElement(By.Name("group_footer")).Clear();
        _driver.FindElement(By.Name("group_footer")).SendKeys(group.Footer);
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
        _driver.FindElement(By.XPath("//span[" + index + "]/input")).Click();
        return this;
    }


    public GroupHelper ReturnToGroupsPage()
    {
        _driver.FindElement(By.LinkText("group page")).Click();
        return this;
    }
}
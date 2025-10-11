using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using NUnit.Framework;
using OpenQA.Selenium.Support.UI;

namespace addressbook_web_tests;

public class GroupHelper : HelperBase
{
   public GroupHelper(IWebDriver driver) : base(driver)
    {
        this._driver = driver;
    }
    
    public void InitNewGroupCreation()
    {
        _driver.FindElement(By.Name("new")).Click();
    }
    
    public void FillGroupForm(GroupData group)
    {
        _driver.FindElement(By.Name("group_name")).Click();
        _driver.FindElement(By.Name("group_name")).Clear();
        _driver.FindElement(By.Name("group_name")).SendKeys(group.Name);
        _driver.FindElement(By.Name("group_header")).Clear();
        _driver.FindElement(By.Name("group_header")).SendKeys(group.Header);
        _driver.FindElement(By.Name("group_footer")).Clear();
        _driver.FindElement(By.Name("group_footer")).SendKeys(group.Footer);
    }
    
    public void SubmitGroupCreation()
    {
        _driver.FindElement(By.Name("submit")).Click();
    }

    public void RemoveGroup()
    {
        _driver.FindElement(By.XPath("//input[@type='submit' and @name='delete' and @value='Delete group(s)']"))
            .Click();
    }
    
    public void SelectGroup(int index)
    {
        _driver.FindElement(By.XPath("//span[" + index + "]/input")).Click();
    }


    public void ReturnToGroupsPage()
    {
        _driver.FindElement(By.LinkText("group page")).Click();
    }

}
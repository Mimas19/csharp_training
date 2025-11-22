using System.Text;
using NUnit.Framework;
using OpenQA.Selenium;

namespace addressbook_web_tests;

public class TestBase
{
    public static bool PERFORM_LONG_UI_CHECKS = false; // false для отключения сравнения с БД
    protected ApplicationManager app;
    
    [SetUp]
    public void SetupApplicationManager()
    {
        app = ApplicationManager.GetInstance();
    }
    
    public static Random rnd = new Random();
    
    public static string GenerateRandomString(int max)
    {
        int l = Convert.ToInt32(rnd.NextDouble() * max);
        StringBuilder builder = new StringBuilder();
        for (int i = 0; i < l; i++)
        {
            builder.Append(Convert.ToChar(32 + Convert.ToInt32(rnd.NextDouble() * 65)));
        }
        return builder.ToString();
    }
    
}
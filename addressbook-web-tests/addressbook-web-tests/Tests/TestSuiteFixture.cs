using System;
using System.Text;
using NUnit.Framework;
using OpenQA.Selenium;
using System.Collections;


namespace addressbook_web_tests;

[SetUpFixture]
public class TestSuiteFixture
{
    public static ApplicationManager app;
    
    [OneTimeSetUp]
    public void InitApplicationManager()
    {
        app = ApplicationManager.GetInstance();
        app.Navigator.OpenHomePage();
        app.Auth.Login(new AccountData("admin","secret"));
    }
    [OneTimeTearDown]
    public void CleanupApplicationManager()
    {
        try
        {
            // Корректно закрываем браузер после завершения всех тестов
            app.Driver.Quit();
            app.Driver.Dispose(); // опционально, если реализован
        }
        catch (Exception ex)
        {
            // Логировать при необходимости
            Console.WriteLine($"Ошибка при закрытии драйвера: {ex.Message}");
        }
    }
}
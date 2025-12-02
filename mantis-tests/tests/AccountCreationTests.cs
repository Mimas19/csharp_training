using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using NUnit.Framework;
using OpenQA.Selenium.Support.UI;

namespace mantis_tests

{
    
    [TestFixture]
    public class AccountCreationTests : TestBase
    {
        [SetUp]
        public void SetupConfig()
        {
            // Бэкап текущей конфигурации
            app.Ftp.BackupFile("/config_inc.php");
            
            using (Stream localFile = File.Open("/config_defaults_inc.php", FileMode.Open))
            {
                app.Ftp.Upload("/config_defaults_inc.php", localFile);
            }
        }
        
        [TearDown]  // Выполняется после каждого теста
        public void RestoreConfig()
        {
            app.Ftp.RestoreBackupFile("/config_inc.php");
        }

        [Test]
        public void TestAccountRegistration()
        {
            AccountData account = new AccountData() 
            {
                Name = "testuser",
                Password = "password",
                Email = "testuser@localhost.localdomain",
            };

            app.James.Delete(account);
            app.James.Add(account);
            
            app.Registration.Register(account);

        }

    }
}
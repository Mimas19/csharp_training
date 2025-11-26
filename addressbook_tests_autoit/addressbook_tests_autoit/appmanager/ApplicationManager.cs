using System;
using System.Diagnostics;
using AutoIt;

namespace addressbook_tests_autoit
{
    public class ApplicationManager
    {
        public static string WINTITLE = "Free Address Book";
        private GroupHelper groupHelper;

        public ApplicationManager()
        {
            // Запуск приложения через Process.Start
            Process.Start(@"C:\Users\mislimova\Downloads\FreeAddressBookPortable");

            AutoItX.WinWait(WINTITLE);
            AutoItX.WinActivate(WINTITLE);
            AutoItX.WinWaitActive(WINTITLE);

            groupHelper = new GroupHelper(this);
        }

        public void Stop()
        {
            AutoItX.ControlClick(WINTITLE, "", "WindowsForms10.BUTTON.app.0.2c908d510");
        }

        public GroupHelper Groups
        {
            get { return groupHelper; }
        }
    }
}
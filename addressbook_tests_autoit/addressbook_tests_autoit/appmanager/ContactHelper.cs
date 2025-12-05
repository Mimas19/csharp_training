using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoIt;

namespace addressbook_tests_autoit
{
    public class ContactHelper : HelperBase
    {
        public static string CONTACTEDITORWINTITLE = "Contact Editor";
        public static string QUESTIONWINTITLE = "Question";

        public ContactHelper(ApplicationManager manager) : base(manager)
        {
        }

        public ContactHelper Create(ContactData contact)
        {
            InitContactCreation();
            FillContactForm(contact);
            SubmitContactCreationAndCloseDialogue();
            return this;
        }

        public ContactHelper Remove(int index)
        {
            SelectContact(index);
            InitContactRemoval();
            SubmitContactRemoval();
            return this;
        }

        // Contact creation methods
        public void InitContactCreation()
        {
            AutoItX.ControlClick(WINTITLE, "", "WindowsForms10.BUTTON.app.0.2c908d58");
            AutoItX.WinWait(CONTACTEDITORWINTITLE);
        }

        public void FillContactForm(ContactData contact)
        {
            AutoItX.ControlFocus(CONTACTEDITORWINTITLE, "", "WindowsForms10.EDIT.app.0.2c908d516");
            AutoItX.Send(contact.Firstname);
            AutoItX.ControlFocus(CONTACTEDITORWINTITLE, "", "WindowsForms10.EDIT.app.0.2c908d513");
            AutoItX.Send(contact.Lastname);
        }

        public void SubmitContactCreationAndCloseDialogue()
        {
            AutoItX.ControlClick(CONTACTEDITORWINTITLE, "", "WindowsForms10.BUTTON.app.0.2c908d58");
        }

        private void SelectContact(int index)
        {
           AutoItX.ControlListView(WINTITLE, "", "WindowsForms10.Window.8.app.0.2c908d510",
                "Select", index.ToString(), "");
        }

      private void InitContactRemoval()
        {
            AutoItX.ControlClick(WINTITLE, "", "WindowsForms10.BUTTON.app.0.2c908d59");
            AutoItX.WinWait(QUESTIONWINTITLE);
        }

        private void SubmitContactRemoval()
        {
            AutoItX.ControlClick(QUESTIONWINTITLE, "", "WindowsForms10.BUTTON.app.0.2c908d52");
        }

       public List<ContactData> GetContactsList()
        {
            List<ContactData> list = new List<ContactData>();
            int count = GetContactCount();

            for (int i = 0; i < count; i++)
            {
                string firstName = AutoItX.ControlListView(
                    WINTITLE, "", "WindowsForms10.Window.8.app.0.2c908d510",
                    "GetText", i.ToString(), "0");
                string lastName = AutoItX.ControlListView(
                    WINTITLE, "", "WindowsForms10.Window.8.app.0.2c908d510",
                    "GetText", i.ToString(), "1");
                list.Add(new ContactData()
                {
                    Firstname = firstName,
                    Lastname = lastName
                });
            }
            return list;
        }

        public void VerifyContactPresence()
        {
            if (GetContactCount() < 1)
            {
                ContactData newContact = new ContactData()
                {
                    Firstname = "FirstName",
                    Lastname = "LastName"
                };
                Create(newContact);
            }
        }

        public int GetContactCount()
        {
            return int.Parse(AutoItX.ControlListView(
                WINTITLE, "", "WindowsForms10.Window.8.app.0.2c908d510",
                "GetItemCount", "", ""));
        }
    }
}
using System.Collections.Generic;
using AutoIt;

namespace addressbook_tests_autoit
{
    public class GroupHelper : HelperBase
    {
        public static string GROUPWINTITLE = "Group editor";

        public GroupHelper(ApplicationManager manager) : base(manager)
        {
        }

        public List<GroupData> GetGroupList()
        {
            List<GroupData> list = new List<GroupData>();
            OpenGroupsDialogue();

            string count = AutoItX.ControlTreeView(
                GROUPWINTITLE, "", "WindowsForms10.SysTreeView32.app.0.2c908d51",
                "GetItemCount", "#0", "");

            for (int i = 0; i < int.Parse(count); i++)
            {
                string item = AutoItX.ControlTreeView(
                    GROUPWINTITLE, "", "WindowsForms10.SysTreeView32.app.0.2c908d51",
                    "GetText", "#0|#" + i, "");
                list.Add(new GroupData()
                {
                    Name = item,
                });
            }
            CloseGroupDialogue();
            return list;
        }

        public void Add(GroupData newGroup)
        {
            OpenGroupsDialogue();
            AutoItX.ControlClick(GROUPWINTITLE, "", "WindowsForms10.BUTTON.app.0.2c908d53");
            AutoItX.Send(newGroup.Name);
            AutoItX.Send("{ENTER}");
            CloseGroupDialogue();
        }

        public void CloseGroupDialogue()
        {
            AutoItX.ControlClick(GROUPWINTITLE, "", "WindowsForms10.BUTTON.app.0.2c908d54");
        }

        public void OpenGroupsDialogue()
        {
            AutoItX.ControlClick(WINTITLE, "", "WindowsForms10.BUTTON.app.0.2c908d512");
            AutoItX.WinWait(GROUPWINTITLE);
        }
        
        
        public void Remove(string groupName)
        {
            OpenGroupsDialogue();
    
            // Выбираем группу в дереве
            AutoItX.ControlTreeView(
                GROUPWINTITLE, "", "WindowsForms10.SysTreeView32.app.0.2c908d51",
                "Select", "#0|#" + FindGroupIndex(groupName), "");
    
            // Нажимаем Delete
            AutoItX.ControlClick(GROUPWINTITLE, "", "WindowsForms10.BUTTON.app.0.2c908d51");
            
            //?????????????????????????????????????? вот тут ещё какое-то действие?
            // меня смущает немного диалоговое окно и как с ним работать с удалением
    
            // Подтверждаем удаление (OK)
            AutoItX.ControlClick("", "", "WindowsForms10.BUTTON.app.0.2c908d53");
    
            CloseGroupDialogue();
        }

        private int FindGroupIndex(string groupName)
        {
            string count = AutoItX.ControlTreeView(
                GROUPWINTITLE, "", "WindowsForms10.SysTreeView32.app.0.2c908d51",
                "GetItemCount", "#0", "");

            for (int i = 0; i < int.Parse(count); i++)
            {
                string item = AutoItX.ControlTreeView(
                    GROUPWINTITLE, "", "WindowsForms10.SysTreeView32.app.0.2c908d51",
                    "GetText", "#0|#" + i, "");
        
                if (item == groupName)
                    return i;
            }
            return -1;
        }

    }
}
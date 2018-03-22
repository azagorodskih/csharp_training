using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace addressbook_tests_autoit
{
    public class GroupHelper : HelperBase
    {
        public static string GROUPWINTITLE = "Group editor";
        public static string DELETEWINTITLE = "Delete group";

        public GroupHelper(ApplicationManager manager) : base(manager) { }

        public void Add(GroupData group)
        {
            OpenGroupsDialogue();
            aux.ControlClick(GROUPWINTITLE, "", "WindowsForms10.BUTTON.app.0.3dd72c23");
            aux.Send(group.Name);
            aux.Send("{ENTER}");
            CloseGroupsDialogue();
        }

        public void Remove(int index, bool isMoveContacts)
        {
            OpenGroupsDialogue();
            SelectGroup(GROUPWINTITLE, "WindowsForms10.SysTreeView32.app.0.3dd72c21", index);
            aux.ControlClick(GROUPWINTITLE, "", "WindowsForms10.BUTTON.app.0.3dd72c21");
            aux.WinWait(DELETEWINTITLE);
            if (isMoveContacts)
            {
                aux.ControlClick(DELETEWINTITLE, "", "WindowsForms10.BUTTON.app.0.3dd72c22");
                SelectGroup(DELETEWINTITLE, "WindowsForms10.SysTreeView32.app.0.3dd72c21", 0);
            }
            else
            {
                aux.ControlClick(DELETEWINTITLE, "", "WindowsForms10.BUTTON.app.0.3dd72c21");
            }
            aux.ControlClick(DELETEWINTITLE, "", "WindowsForms10.BUTTON.app.0.3dd72c23");
            aux.WinWait(GROUPWINTITLE);
            CloseGroupsDialogue();
        }

        public List<GroupData> GetGroupList()
        {
            List<GroupData> groups = new List<GroupData>();

            OpenGroupsDialogue();
            string count = aux.ControlTreeView(GROUPWINTITLE, "", "WindowsForms10.SysTreeView32.app.0.3dd72c21",
                "GetItemCount", "#0", "");
            for (int i = 0; i < int.Parse(count); i++)
            {
                string item = aux.ControlTreeView(GROUPWINTITLE, "", "WindowsForms10.SysTreeView32.app.0.3dd72c21",
                "GetText", "#0|#" + i, "");
                groups.Add(new GroupData() {
                    Name = item
                });
            }
            CloseGroupsDialogue();
            return groups;
        }

        private void SelectGroup(string winTitleName, string treeViewId, int index)
        {
            string item = aux.ControlTreeView(winTitleName, "", treeViewId, "Select", "#0|#" + index, "");
        }

        private void OpenGroupsDialogue()
        {
            aux.ControlClick(WINTITLE, "", "WindowsForms10.BUTTON.app.0.3dd72c212");
            aux.WinWait(GROUPWINTITLE);
        }

        private void CloseGroupsDialogue()
        {
            aux.ControlClick(GROUPWINTITLE, "", "WindowsForms10.BUTTON.app.0.3dd72c24");
        }
    }
}
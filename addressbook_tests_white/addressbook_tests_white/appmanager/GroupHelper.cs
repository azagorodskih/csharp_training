using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestStack.White;
using TestStack.White.UIItems;
using TestStack.White.UIItems.WindowItems;
using TestStack.White.UIItems.TreeItems;
using TestStack.White.UIItems.Finders;
using System.Windows.Automation;
using TestStack.White.InputDevices;
using TestStack.White.WindowsAPI;

namespace addressbook_tests_white
{
    public class GroupHelper : HelperBase
    {
        public static string GROUPWINTITLE = "Group editor";

        public GroupHelper(ApplicationManager manager) : base(manager) { }

        public void Add(GroupData group)
        {
            Window dialogue = OpenGroupsDialogue();
            dialogue.Get<Button>("uxNewAddressButton").Click();
            TextBox textBox = (TextBox) dialogue.Get(SearchCriteria.ByControlType(ControlType.Edit));
            textBox.Enter(group.Name);
            Keyboard.Instance.PressSpecialKey(KeyboardInput.SpecialKeys.RETURN);
            CloseGroupsDialogue(dialogue);
        }

        public List<GroupData> GetGroupList()
        {
            List<GroupData> groups = new List<GroupData>();

            Window dialogue = OpenGroupsDialogue();
            Tree tree = dialogue.Get<Tree>("uxAddressTreeView");
            TreeNode root = tree.Nodes[0];
            foreach (TreeNode item in root.Nodes)
            {
                groups.Add(new GroupData()
                {
                    Name = item.Text
                });
            }
            CloseGroupsDialogue(dialogue);
            return groups;
        }

        private Window OpenGroupsDialogue()
        {
            manager.MainWindow.Get<Button>("groupButton").Click();
            return manager.MainWindow.ModalWindow(GROUPWINTITLE);
        }

        private void CloseGroupsDialogue(Window dialogue)
        {
            dialogue.Get<Button>("uxCloseAddressButton").Click();
        }
    }
}
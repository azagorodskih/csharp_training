using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using System.Collections.Generic;

namespace addressbook_tests_autoit
{
    [TestFixture]
    public class GroupRemovalTests : TestBase
    {
        [Test]
        //удалить группу с перемещением её контактов в другую группу
        public void TestGroupRemoval_RemoveWithMovingContacts()
        {
            int index = 0;
            bool isMoveContacts = true;
            List<GroupData> oldGroups = app.Groups.GetGroupList();

            if (oldGroups.Count > 1)
            {
                app.Groups.Remove(index, isMoveContacts);
                oldGroups.RemoveAt(index);

                List<GroupData> newGroups = app.Groups.GetGroupList();
                oldGroups.Sort();
                newGroups.Sort();
                Assert.AreEqual(oldGroups, newGroups);
            }
            else
            {
                Console.Out.Write("Удаление невозможно! В списке групп должна присутствовать хотя бы одна группа!");
            }
        }

        [Test]
        //удалить группу без перемещения её контактов в другую группу
        public void TestGroupRemoval_RemoveWithoutMovingContacts()
        {
            int index = 0;
            bool isMoveContacts = false;
            List<GroupData> oldGroups = app.Groups.GetGroupList();

            if (oldGroups.Count > 1)
            {
                app.Groups.Remove(index, isMoveContacts);
                oldGroups.RemoveAt(index);

                List<GroupData> newGroups = app.Groups.GetGroupList();
                oldGroups.Sort();
                newGroups.Sort();
                Assert.AreEqual(oldGroups, newGroups);
            }
            else
            {
                Console.Out.Write("Удаление невозможно! В списке групп должна присутствовать хотя бы одна группа!");
            }
        }
    }
}

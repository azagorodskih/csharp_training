using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using System.Collections.Generic;

namespace WebAddressbookTests
{
    [TestFixture]
    public class GroupRemovalTests : AuthTestBase
    {
        [Test]
        //удалить одну группу
        public void GroupRemovalTest_RemoveOne()
        {
            List<int> Index = new List<int>();
            Index.Add(1);

            List<GroupData> oldGroups = app.Groups.GetGroupList();

            if (oldGroups.Count > 0)
            {
                if (! app.Groups.IsGroupPresent(Index[0]))
                {
                    Index[0] = 0;
                }
                app.Groups.RemoveGroup(Index);
                oldGroups.RemoveAt(Index[0]);
            }
            else
            {
                app.Groups.Create(new GroupData(""));
                Index[0] = 0;
                app.Groups.RemoveGroup(Index);
            }

            List<GroupData> newGroups = app.Groups.GetGroupList();
            oldGroups.Sort();
            newGroups.Sort();
            Assert.AreEqual(oldGroups, newGroups);

            //app.Auth.Logout();
        }

        [Test]
        //удалить несколько групп
        public void GroupRemovalTest_RemoveSeveral()
        {
            List<int> Index = new List<int>(); //общий список индексов, запрашиваемых к удалению
            Index.Add(2);
            Index.Add(4);
            Index.Add(1);
            List<int> correctIndex = new List<int>(); //список существующих индексов, запрашиваемых к удалению
            List<GroupData> oldGroups_Before = app.Groups.GetGroupList(); //начальный список групп до удаления
            List<GroupData> oldGroups_After = new List<GroupData>(); //начальный список групп после удаления

            if (oldGroups_Before.Count > 0)
            {
                foreach (int i in Index)
                {
                    if (app.Groups.IsGroupPresent(i))
                    {
                        correctIndex.Add(i);
                    }
                }
                if (correctIndex.Count == 0)
                {
                    correctIndex.Add(0);
                }

                app.Groups.RemoveGroup(correctIndex);
                
                for(int i = 0; i < oldGroups_Before.Count; i++)
                {
                    if (! correctIndex.Contains(i))
                    {
                        oldGroups_After.Add(oldGroups_Before[i]);
                    }
                }
            }
            else
            {
                app.Groups.Create(new GroupData(""));
                correctIndex.Add(0);
                app.Groups.RemoveGroup(correctIndex);
            }
            
            List<GroupData> newGroups = app.Groups.GetGroupList();
            oldGroups_After.Sort();
            newGroups.Sort();
            Assert.AreEqual(oldGroups_After, newGroups);

            //app.Auth.Logout();
        }
    }
}

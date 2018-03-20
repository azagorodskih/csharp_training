using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using System.Collections.Generic;

namespace WebAddressbookTests
{
    [TestFixture]
    public class GroupRemovalTests : GroupTestBase
    {
        [Test]
        //удалить одну группу
        public void GroupRemovalTest_RemoveOne()
        {
            List<int> Index = new List<int>();
            Index.Add(0); //отсчет от 0; для упрощения проверки теста удалению будет подвергаться первая группа

            //List<GroupData> oldGroups = app.Groups.GetGroupList();
            List<GroupData> oldGroups = GroupData.GetAll();

            if (oldGroups.Count == 0)
            {
                app.Groups.Create(new GroupData("NAME"));
                //oldGroups.Add(new GroupData("NAME"));
                oldGroups = GroupData.GetAll(); //app.Groups.GetGroupList(); //чтобы также узнать идентификатор созданной группы
            }

            //app.Groups.Remove(Index);
            GroupData toBeRemoved = oldGroups[Index[0]];
            app.Groups.Remove(toBeRemoved);
            oldGroups.RemoveAt(Index[0]);

            //Assert.AreEqual(oldGroups.Count, app.Groups.GetGroupCount());

            //List<GroupData> newGroups = app.Groups.GetGroupList();
            List<GroupData> newGroups = GroupData.GetAll();
            oldGroups.Sort();
            newGroups.Sort();
            Assert.AreEqual(oldGroups, newGroups);

            foreach (GroupData group in newGroups)
            {
                Assert.AreNotEqual(group.Id, toBeRemoved.Id);
            }

            //app.Auth.Logout();
        }

        [Test]
        //удалить несколько групп
        public void GroupRemovalTest_RemoveSeveral()
        {
            List<int> Index = new List<int>(); //общий список индексов, запрашиваемых к удалению
            Index.Add(2);
            Index.Add(1);
            Index.Add(3);
            List<GroupData> oldGroups_Before = app.Groups.GetGroupList(); //список групп до удаления
            List<GroupData> oldGroups_After = new List<GroupData>(); //список групп после удаления

            int groupCount = oldGroups_Before.Count;
            foreach (int i in Index)
            {
                if (!app.Groups.IsGroupPresent(i))
                {
                    do
                    {
                        app.Groups.Create(new GroupData("NAME" + i));
                        //oldGroups_Before.Add(new GroupData("NAME" + i));
                        groupCount++;
                    }
                    while ((groupCount - 1) != i);
                    /*oldGroups_Before.Sort(); /*сортировка сделана потому, что после добавления новой группы 
                                        они автоматически сортируются по фамилии (видно в браузере),
                                        и в дальнейшем после удаления списки oldGroups и newGroups могут разойтись из-за этой особенности*/
                }
            }
            oldGroups_Before = app.Groups.GetGroupList(); //чтобы также узнать идентификатор созданной группы

            app.Groups.Remove(Index);

            /*После использования RemoveAt в списке происходит сдвиг элементов.
            Поэтому чтобы правильно сформировать в oldGroups список оставшихся после удаления групп,
            перепишем в новый список те группы, которые не запрашивались для удаления*/
            for (int i = 0; i < oldGroups_Before.Count; i++)
            {
                if (!Index.Contains(i))
                {
                    oldGroups_After.Add(oldGroups_Before[i]);
                }
            }

            //Assert.AreEqual(oldGroups_After.Count, app.Groups.GetGroupCount());

            List<GroupData> newGroups = app.Groups.GetGroupList();
            oldGroups_After.Sort();
            newGroups.Sort();
            Assert.AreEqual(oldGroups_After, newGroups);

            for (int i = 0; i < newGroups.Count; i++)
            {
                Assert.AreEqual(newGroups[i].Id, oldGroups_After[i].Id);
            }

            //app.Auth.Logout();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class GroupModificationTests : GroupTestBase
    {
        [Test]
        //замена старых значений новыми
        public void GroupModificationTest_NewData()
        {
            GroupData newData = new GroupData("mmm");
            newData.Header = "nnn";
            newData.Footer = "ooo";
            int index = 0; //отсчет от 0; для упрощения проверки теста модификации будет подвергаться первая группа

            //List<GroupData> oldGroups = app.Groups.GetGroupList();
            List<GroupData> oldGroups = GroupData.GetAll();

            if (oldGroups.Count == 0)
            {
                app.Groups.Create(new GroupData("NAME"));
                //oldGroups.Add(new GroupData("NAME"));
                oldGroups = GroupData.GetAll(); //app.Groups.GetGroupList(); //чтобы также узнать идентификатор созданной группы
            }

            //app.Groups.Modify(index, newData);
            GroupData toBeModified = oldGroups[index];
            app.Groups.Modify(toBeModified, newData);
            oldGroups[index].Name = newData.Name;

            //Assert.AreEqual(oldGroups.Count, app.Groups.GetGroupCount());

            //List<GroupData> newGroups = app.Groups.GetGroupList();
            List<GroupData> newGroups = GroupData.GetAll();
            //oldGroups[index].Name = newData.Name;
            oldGroups.Sort();
            newGroups.Sort();
            Assert.AreEqual(oldGroups, newGroups);

            foreach (GroupData group in newGroups)
            {
                if (group.Id == toBeModified.Id)
                {
                    Assert.AreEqual(newData.Name, group.Name);
                }
            }

            //app.Auth.Logout();
        }

        [Test]
        //замена старых значений пустыми
        public void GroupModificationTest_EmptyData()
        {
            GroupData emptyData = new GroupData("");
            emptyData.Header = "";
            emptyData.Footer = "";
            int index = 0; //отсчет от 0; для упрощения проверки теста модификации будет подвергаться первая группа

            //List<GroupData> oldGroups = app.Groups.GetGroupList();
            List<GroupData> oldGroups = GroupData.GetAll();

            if (oldGroups.Count == 0)
            {
                app.Groups.Create(new GroupData("NAME"));
                //oldGroups.Add(new GroupData("NAME"));
                oldGroups = GroupData.GetAll(); //app.Groups.GetGroupList(); //чтобы также узнать идентификатор созданной группы
            }

            //app.Groups.Modify(index, emptyData);
            GroupData toBeModified = oldGroups[index];
            app.Groups.Modify(toBeModified, emptyData);
            oldGroups[index].Name = emptyData.Name;

            //Assert.AreEqual(oldGroups.Count, app.Groups.GetGroupCount());

            //List<GroupData> newGroups = app.Groups.GetGroupList();
            List<GroupData> newGroups = GroupData.GetAll();
            //oldGroups[index].Name = newData.Name;
            oldGroups.Sort();
            newGroups.Sort();
            Assert.AreEqual(oldGroups, newGroups);

            foreach (GroupData group in newGroups)
            {
                if (group.Id == toBeModified.Id)
                {
                    Assert.AreEqual(emptyData.Name, group.Name);
                }
            }

            //app.Auth.Logout();
        }

        [Test]
        //частичная замена старых значений новыми
        public void GroupModificationTest_SomeFields()
        {
            GroupData newData = new GroupData("bbb");
            newData.Header = null;
            newData.Footer = null;
            int index = 0; //отсчет от 0; для упрощения проверки теста модификации будет подвергаться первая группа

            //List<GroupData> oldGroups = app.Groups.GetGroupList();
            List<GroupData> oldGroups = GroupData.GetAll();

            if (oldGroups.Count == 0)
            {
                app.Groups.Create(new GroupData("NAME"));
                //oldGroups.Add(new GroupData("NAME"));
                oldGroups = GroupData.GetAll(); //app.Groups.GetGroupList(); //чтобы также узнать идентификатор созданной группы
            }

            //app.Groups.Modify(index, newData);
            GroupData toBeModified = oldGroups[index];
            app.Groups.Modify(toBeModified, newData);
            oldGroups[index].Name = newData.Name;

            //Assert.AreEqual(oldGroups.Count, app.Groups.GetGroupCount());

            //List<GroupData> newGroups = app.Groups.GetGroupList();
            List<GroupData> newGroups = GroupData.GetAll();
            //oldGroups[index].Name = newData.Name;
            oldGroups.Sort();
            newGroups.Sort();
            Assert.AreEqual(oldGroups, newGroups);

            foreach (GroupData group in newGroups)
            {
                if (group.Id == toBeModified.Id)
                {
                    Assert.AreEqual(newData.Name, group.Name);
                }
            }

            //app.Auth.Logout();
        }
    }
}

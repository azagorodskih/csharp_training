using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class GroupModificationTests : AuthTestBase
    {
        [Test]
        //замена старых значений новыми
        public void GroupModificationTest_NewData()
        {
            GroupData newData = new GroupData("mmm");
            newData.Header = "nnn";
            newData.Footer = "ooo";
            int index = 0; //отсчет от 0; для упрощения проверки теста модификации будет подвергаться первая группа

            List<GroupData> oldGroups = app.Groups.GetGroupList();

            if(oldGroups.Count == 0)
            {
                app.Groups.Create(new GroupData("NAME"));
                oldGroups.Add(new GroupData("NAME"));
            }

            app.Groups.Modify(index, newData);
            oldGroups[index].Name = newData.Name;

            List<GroupData> newGroups = app.Groups.GetGroupList();
            //oldGroups[index].Name = newData.Name;
            oldGroups.Sort();
            newGroups.Sort();
            Assert.AreEqual(oldGroups, newGroups);

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

            List<GroupData> oldGroups = app.Groups.GetGroupList();

            if (oldGroups.Count == 0)
            {
                app.Groups.Create(new GroupData("NAME"));
                oldGroups.Add(new GroupData("NAME"));
            }

            app.Groups.Modify(index, emptyData);
            oldGroups[index].Name = emptyData.Name;

            List<GroupData> newGroups = app.Groups.GetGroupList();
            //oldGroups[index].Name = newData.Name;
            oldGroups.Sort();
            newGroups.Sort();
            Assert.AreEqual(oldGroups, newGroups);

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

            List<GroupData> oldGroups = app.Groups.GetGroupList();

            //if (!app.Groups.IsGroupPresent(index))
            //{
            //    GroupData defGroup = new GroupData("NAME");
            //    do
            //    {
            //        app.Groups.Create(defGroup);
            //        oldGroups.Add(defGroup);
            //    }
            //    while ((oldGroups.Count - 1) != index);
            //    oldGroups.Sort(); /*сортировка сделана потому, что после добавления новой группы 
            //                        они автоматически сортируются по наименованию (видно в браузере),
            //                        и в дальнейшем после модификации списки oldGroups и newGroups могут разойтись из-за этой особенности*/
            //}

            if (oldGroups.Count == 0)
            {
                app.Groups.Create(new GroupData("NAME"));
                oldGroups.Add(new GroupData("NAME"));
            }

            app.Groups.Modify(index, newData);
            oldGroups[index].Name = newData.Name;

            List<GroupData> newGroups = app.Groups.GetGroupList();
            //oldGroups[index].Name = newData.Name;
            oldGroups.Sort();
            newGroups.Sort();
            Assert.AreEqual(oldGroups, newGroups);
            
            //app.Auth.Logout();
        }
    }
}

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
            int index = 5; //отсчет от 0

            List<GroupData> oldGroups = app.Groups.GetGroupList();

            if(oldGroups.Count > 0)
            {
                if(! app.Groups.IsGroupPresent(index))
                {
                    index = 0;
                }
                app.Groups.Modify(index, newData);
                oldGroups[index].Name = newData.Name;
            }
            else
            {
                app.Groups.Create(newData);
                oldGroups.Add(newData);
            }

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
            int index = 1; //отсчет от 0

            List<GroupData> oldGroups = app.Groups.GetGroupList();

            if (oldGroups.Count > 0)
            {
                if (! app.Groups.IsGroupPresent(index))
                {
                    index = 0;
                }
                app.Groups.Modify(index, emptyData);
                oldGroups[index].Name = emptyData.Name;
            }
            else
            {
                app.Groups.Create(emptyData);
                oldGroups.Add(emptyData);
            }

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
            int index = 5; //отсчет от 0

            List<GroupData> oldGroups = app.Groups.GetGroupList();

            if (oldGroups.Count > 0)
            {
                if (! app.Groups.IsGroupPresent(index))
                {
                    index = 0;
                }
                app.Groups.Modify(index, newData);
                oldGroups[index].Name = newData.Name;
            }
            else
            {
                app.Groups.Create(newData);
                oldGroups.Add(newData);
            }

            List<GroupData> newGroups = app.Groups.GetGroupList();
            //oldGroups[index].Name = newData.Name;
            oldGroups.Sort();
            newGroups.Sort();
            Assert.AreEqual(oldGroups, newGroups);
            
            //app.Auth.Logout();
        }
    }
}

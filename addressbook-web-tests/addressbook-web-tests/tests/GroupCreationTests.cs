using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class GroupCreationTests : TestBase
    {
        [Test]
        //заполнить все поля
        public void GroupCreationTest_AllFields()
        {
            GroupData group = new GroupData("aaa");
            group.Header = "bbb";
            group.Footer = "ccc";
            
            app.Groups.Create(group);

            app.Auth.Logout();
        }

        [Test]
        //оставить все поля пустыми
        public void GroupCreationTest_EmptyFields()
        {
            GroupData group = new GroupData("");
            group.Header = "";
            group.Footer = "";
            
            app.Groups.Create(group);

            app.Auth.Logout();
        }

        [Test]
        //заполнить поля спецсимволами
        public void GroupCreationTest_SpecCharFields()
        {
            GroupData group = new GroupData("$$$");
            group.Header = "***";
            group.Footer = "&&&";

            app.Groups.Create(group);

            app.Auth.Logout();
        }
    }
}

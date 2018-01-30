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
        public void GroupCreationTest()
        {
            navigationHelper.OpenHomePage();
            authHelper.Login(new AccountData("admin", "secret"));
            navigationHelper.GoToGroupsPage();
            groupHelper.InitNewGroupCreation();

            //FillGroupForm(new GroupData("nnn"));
            GroupData group = new GroupData("nnn");
            group.Header = "hhh";
            group.Footer = "fff";
            groupHelper.FillGroupForm(group);

            groupHelper.SubmitGroupCreation();
            groupHelper.ReturnToGroupsPage();
            authHelper.Logout();
        }
    }
}

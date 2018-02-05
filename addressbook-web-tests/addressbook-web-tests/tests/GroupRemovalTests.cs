using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class GroupRemovalTests : TestBase
    {
        [Test]
        //удалить одну группу
        public void GroupRemovalTest_RemoveOne()
        {
            int[] index = new int[] { 1 };

            app.Groups.Remove(index);
            //app.Auth.Logout();
        }

        [Test]
        //удалить несколько групп
        public void GroupRemovalTest_RemoveSeveral()
        {
            int[] index = new int[] { 2, 3 };

            app.Groups.Remove(index);
            //app.Auth.Logout();
        }
    }
}

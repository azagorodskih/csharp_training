﻿using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class GroupRemovalTests : AuthTestBase
    {
        [Test]
        //удалить одну группу
        public void GroupRemovalTest_RemoveOne()
        {
            int[] index = new int[] { 2 };

            app.Groups.RemoveGroup(index);
            //app.Auth.Logout();
        }

        [Test]
        //удалить несколько групп
        public void GroupRemovalTest_RemoveSeveral()
        {
            int[] index = new int[] { 1, 5, 2 };

            app.Groups.RemoveGroup(index);
            //app.Auth.Logout();
        }
    }
}

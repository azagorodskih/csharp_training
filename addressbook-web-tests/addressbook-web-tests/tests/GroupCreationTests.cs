﻿using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using System.Collections.Generic;

namespace WebAddressbookTests
{
    [TestFixture]
    public class GroupCreationTests : AuthTestBase
    {
        public static IEnumerable<GroupData> RandomGroupDataProvider()
        {
            List<GroupData> groups = new List<GroupData>();
            for (int i = 0; i < 5; i++)
            {
                groups.Add(new GroupData(GenerateRandomString(30))
                {
                    Header = GenerateRandomString(100),
                    Footer = GenerateRandomString(100)
                });
            }

            return groups;
        }

        [Test, TestCaseSource("RandomGroupDataProvider")]
        //заполнить все поля
        public void GroupCreationTest_AllFields(GroupData group)
        {
            //GroupData group = new GroupData("aaa")
            //{
            //    Header = "bbb",
            //    Footer = "ccc"
            //};

            List<GroupData> oldGroups = app.Groups.GetGroupList();

            app.Groups.Create(group);
            oldGroups.Add(group);

            //Assert.AreEqual(oldGroups.Count + 1, app.Groups.GetGroupCount());

            List<GroupData> newGroups = app.Groups.GetGroupList();
            oldGroups.Sort();
            newGroups.Sort();
            Assert.AreEqual(oldGroups, newGroups);

            //app.Auth.Logout();
        }

        //[Test]
        ////оставить все поля пустыми
        //public void GroupCreationTest_EmptyFields()
        //{
        //    GroupData group = new GroupData("")
        //    {
        //        Header = "",
        //        Footer = ""
        //    };

        //    List<GroupData> oldGroups = app.Groups.GetGroupList();

        //    app.Groups.Create(group);

        //    //Assert.AreEqual(oldGroups.Count + 1, app.Groups.GetGroupCount());

        //    List<GroupData> newGroups = app.Groups.GetGroupList();
        //    oldGroups.Add(group);
        //    oldGroups.Sort();
        //    newGroups.Sort();
        //    Assert.AreEqual(oldGroups, newGroups);

        //    //app.Auth.Logout();
        //}

        //[Test]
        ////заполнить поля спецсимволами
        //public void GroupCreationTest_SpecCharFields()
        //{
        //    GroupData group = new GroupData("$$$")
        //    {
        //        Header = "***",
        //        Footer = "&&&"
        //    };

        //    List<GroupData> oldGroups = app.Groups.GetGroupList();

        //    app.Groups.Create(group);

        //    //Assert.AreEqual(oldGroups.Count + 1, app.Groups.GetGroupCount());

        //    List<GroupData> newGroups = app.Groups.GetGroupList();
        //    oldGroups.Add(group);
        //    oldGroups.Sort();
        //    newGroups.Sort();
        //    Assert.AreEqual(oldGroups, newGroups);

        //    //app.Auth.Logout();
        //}

        //[Test]
        //заполнить все поля
        //public void GroupCreationTest_BadName()
        //{
        //    GroupData group = new GroupData("a'a");
        //    group.Header = "bbb";
        //    group.Footer = "ccc";

        //    List<GroupData> oldGroups = app.Groups.GetGroupList();

        //    app.Groups.Create(group);

        //    //Assert.AreEqual(oldGroups.Count + 1, app.Groups.GetGroupCount());

        //    List<GroupData> newGroups = app.Groups.GetGroupList();
        //    oldGroups.Add(group);
        //    oldGroups.Sort();
        //    newGroups.Sort();
        //    Assert.AreEqual(oldGroups, newGroups);

        //    //app.Auth.Logout();
        //}
    }
}

using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace WebAddressbookTests
{
    [TestFixture]
    public class GroupCreationTests : AuthTestBase
    {
        //генератор случайных строк
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

        //чтение данных из файла .csv
        public static IEnumerable<GroupData> GroupDataFromCsvFile()
        {
            List<GroupData> groups = new List<GroupData>();
            string[] lines = File.ReadAllLines(@"groups.csv");
            foreach (string l in lines)
            {
                string[] parts = l.Split(';');
                groups.Add(new GroupData(parts[0])
                {
                    Header = parts[1],
                    Footer = parts[2]
                });
            }

            return groups;
        }

        //чтение данных из файла .xml
        public static IEnumerable<GroupData> GroupDataFromXmlFile()
        {
            return (List<GroupData>) 
                new XmlSerializer(typeof(List<GroupData>))
                .Deserialize(new StreamReader(@"groups.xml"));
        }

        //чтение данных из файла .json
        public static IEnumerable<GroupData> GroupDataFromJsonFile()
        {
            return JsonConvert.DeserializeObject<List<GroupData>>(
                File.ReadAllText(@"groups.json"));
        }

        //чтение данных из файла .xls
        //public static IEnumerable<GroupData> GroupDataFromXlsFile()
        //{

        //}

        //заполнить все поля; данные из файла .xml
        [Test, TestCaseSource("GroupDataFromXmlFile")]
        public void GroupCreationTest_AllFieldsXml(GroupData group)
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

        //заполнить все поля; данные из файла .json
        [Test, TestCaseSource("GroupDataFromJsonFile")]
        public void GroupCreationTest_AllFieldsJson(GroupData group)
        {
            List<GroupData> oldGroups = app.Groups.GetGroupList();

            app.Groups.Create(group);
            oldGroups.Add(group);

            List<GroupData> newGroups = app.Groups.GetGroupList();
            oldGroups.Sort();
            newGroups.Sort();
            Assert.AreEqual(oldGroups, newGroups);
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

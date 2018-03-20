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
using Excel = Microsoft.Office.Interop.Excel;
using System.Linq;

namespace WebAddressbookTests
{
    [TestFixture]
    public class GroupCreationTests : GroupTestBase
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

        //чтение данных из файла.xls
        public static IEnumerable<GroupData> GroupDataFromExcelFile()
        {
            List<GroupData> groups = new List<GroupData>();
            Excel.Application app = new Excel.Application();
            Excel.Workbook wb = app.Workbooks.Open(Path.Combine(Directory.GetCurrentDirectory(), @"groups.xlsx"));
            Excel.Worksheet sheet = wb.ActiveSheet;
            Excel.Range range = sheet.UsedRange;
            for (int i = 1; i <= range.Rows.Count; i++)
            {
                groups.Add(new GroupData()
                {
                    Name = range.Cells[i, 1].Value,
                    Header = range.Cells[i, 2].Value,
                    Footer = range.Cells[i, 3].Value
                });
            }
            wb.Close();
            app.Visible = false;
            app.Quit();
            return groups;
        }

        //заполнить все поля; данные из файла .xml
        [Test, TestCaseSource("GroupDataFromXmlFile")]
        public void GroupCreationTest_AllFieldsXml(GroupData group)
        {
            //GroupData group = new GroupData("aaa")
            //{
            //    Header = "bbb",
            //    Footer = "ccc"
            //};

            //List<GroupData> oldGroups = app.Groups.GetGroupList();
            List<GroupData> oldGroups = GroupData.GetAll();

            app.Groups.Create(group);
            oldGroups.Add(group);

            //Assert.AreEqual(oldGroups.Count + 1, app.Groups.GetGroupCount());

            //List<GroupData> newGroups = app.Groups.GetGroupList();
            List<GroupData> newGroups = GroupData.GetAll();
            oldGroups.Sort();
            newGroups.Sort();
            Assert.AreEqual(oldGroups, newGroups);

            //app.Auth.Logout();
        }

        //заполнить все поля; данные из файла .json
        [Test, TestCaseSource("GroupDataFromJsonFile")]
        public void GroupCreationTest_AllFieldsJson(GroupData group)
        {
            //List<GroupData> oldGroups = app.Groups.GetGroupList();
            List<GroupData> oldGroups = GroupData.GetAll();

            app.Groups.Create(group);
            oldGroups.Add(group);

            //List<GroupData> newGroups = app.Groups.GetGroupList();
            List<GroupData> newGroups = GroupData.GetAll();
            oldGroups.Sort();
            newGroups.Sort();
            Assert.AreEqual(oldGroups, newGroups);
        }

        //[Test]
        //public void TestDBConnectivity()
        //{
        //    //DateTime start = DateTime.Now;
        //    //List<GroupData> fromUI = app.Groups.GetGroupList();
        //    //DateTime end = DateTime.Now;
        //    //System.Console.Out.WriteLine(end.Subtract(start));

        //    //start = DateTime.Now;
        //    //List<GroupData> fromDB = GroupData.GetAll();
        //    //end = DateTime.Now;
        //    //System.Console.Out.WriteLine(end.Subtract(start));

        //    foreach(ContactData contact in GroupData.GetAll()[0].GetContacts())
        //    {
        //        System.Console.Out.WriteLine(contact);
        //    }
        //}

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

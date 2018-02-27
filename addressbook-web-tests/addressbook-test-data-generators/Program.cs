using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAddressbookTests;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace addressbook_test_data_generators
{
    class Program
    {
        static void Main(string[] args)
        {
            string dataType = args[0];
            int objectCount = Convert.ToInt32(args[1]);
            StreamWriter writer = new StreamWriter(args[2]);
            string format = args[3];

            List<GroupData> groups = new List<GroupData>();
            List<ContactData> contacts = new List<ContactData>();

            switch (dataType)
            {
                case "group":
                    for (int i = 0; i < objectCount; i++)
                    {
                        groups.Add(new GroupData(TestBase.GenerateRandomString(10))
                        {
                            Header = TestBase.GenerateRandomString(20),
                            Footer = TestBase.GenerateRandomString(20)
                        });
                    }

                    switch (format)
                    {
                        //case "csv":
                        //    writeGroupsToCsvFile(groups, writer);
                        //    break;
                        case "xml":
                            writeGroupsToXmlFile(groups, writer);
                            Console.Out.Write("groups.xml was successfully generated!\n");
                            break;
                        case "json":
                            writeGroupsToJsonFile(groups, writer);
                            Console.Out.Write("groups.json was successfully generated!\n");
                            break;
                        //case "xls":
                        //    writeGroupsToXlsFile();
                        //    break;
                        default:
                            Console.Out.Write("Unrecognized format '" + format + "'.\n");
                            goto Finish;
                    }

                break;
                case "contact":
                    for (int i = 0; i < objectCount; i++)
                    {
                        contacts.Add(new ContactData(TestBase.GenerateRandomString(30), TestBase.GenerateRandomString(30))
                        {
                            Middlename = TestBase.GenerateRandomString(30),
                            Nickname = TestBase.GenerateRandomString(20),
                            Title = TestBase.GenerateRandomString(50),
                            Company = TestBase.GenerateRandomString(50),
                            Address = TestBase.GenerateRandomString(50),
                            Home = TestBase.GenerateRandomString(15),
                            Mobile = TestBase.GenerateRandomString(11),
                            Work = TestBase.GenerateRandomString(15),
                            Email = TestBase.GenerateRandomString(30),
                            Email2 = TestBase.GenerateRandomString(30),
                            Email3 = TestBase.GenerateRandomString(30)
                        });
                    }

                    switch (format)
                    {
                        case "xml":
                            writeContactsToXmlFile(contacts, writer);
                            Console.Out.Write("contacts.xml was successfully generated!\n");
                            break;
                        case "json":
                            writeContactsToJsonFile(contacts, writer);
                            Console.Out.Write("contacts.json was successfully generated!\n");
                            break;
                        default:
                            Console.Out.Write("Unrecognized format '" + format + "'.\n");
                            goto Finish;
                    }

                break;
                default:
                    Console.Out.Write("Unrecognized data type '" + dataType + "'.\n");
                goto Finish;
            }

            //if (format == "csv")
            //{
            //    writeGroupsToCsvFile(groups, writer);
            //}
            //else if (format == "xml")
            //{
            //    writeGroupsToXmlFile(groups, writer);
            //}
            //else if (format == "json")
            //{
            //    writeGroupsToJsonFile(groups, writer);
            //}
            //else
            //{
            //    Console.Out.Write("Unrecognized format " + format);
            //}

            Finish:
                writer.Close();
        }

        static void writeGroupsToCsvFile(List<GroupData> groups, StreamWriter writer)
        {
            foreach (GroupData group in groups)
            {
                writer.WriteLine(String.Format("${0};${1};${2}",
                    group.Name, group.Header, group.Footer));
            }
        }

        static void writeGroupsToXmlFile(List<GroupData> groups, StreamWriter writer)
        {
            new XmlSerializer(typeof(List<GroupData>)).Serialize(writer, groups);
        }

        static void writeGroupsToJsonFile(List<GroupData> groups, StreamWriter writer)
        {
            writer.Write(JsonConvert.SerializeObject(groups, Newtonsoft.Json.Formatting.Indented));
        }

        static void writeGroupsToXlsFile()
        {

        }

        static void writeContactsToXmlFile(List<ContactData> contacts, StreamWriter writer)
        {
            new XmlSerializer(typeof(List<ContactData>)).Serialize(writer, contacts);
        }

        static void writeContactsToJsonFile(List<ContactData> contacts, StreamWriter writer)
        {
            writer.Write(JsonConvert.SerializeObject(contacts, Newtonsoft.Json.Formatting.Indented));
        }
    }
}

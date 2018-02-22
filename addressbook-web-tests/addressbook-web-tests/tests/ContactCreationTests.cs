using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using System.Collections.Generic;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactCreationTests : AuthTestBase
    {
        public static IEnumerable<ContactData> RandomContactDataProvider()
        {
            List<ContactData> groups = new List<ContactData>();
            for (int i = 0; i < 3; i++)
            {
                groups.Add(new ContactData(GenerateRandomString(30), GenerateRandomString(30))
                {
                    Middlename = GenerateRandomString(30),
                    Nickname = GenerateRandomString(20),
                    Title = GenerateRandomString(50),
                    Company = GenerateRandomString(100),
                    Address = GenerateRandomString(100),
                    Home = GenerateRandomString(15),
                    Mobile = GenerateRandomString(11),
                    Work = GenerateRandomString(15),
                    Email = GenerateRandomString(30),
                    Email2 = GenerateRandomString(30),
                    Email3 = GenerateRandomString(30)
                });
            }

            return groups;
        }

        [Test, TestCaseSource("RandomContactDataProvider")]
        //заполнить все поля
        public void ContactCreationTest_AllFields(ContactData contact)
        {
            //ContactData contact = new ContactData("Ilya", "Koblikov")
            //{
            //    Middlename = "A.",
            //    Nickname = "ikoblikov",
            //    Title = "Test Manager",
            //    Company = "VSK",
            //    Address = "Volgograd, Lenina av., 54",
            //    Home = "88442051430",
            //    Mobile = "89173256927",
            //    Work = "88442281940",
            //    Email = "ikoblikov@yandex.ru",
            //    Email2 = "Koblikov@vsk.ru",
            //    Email3 = "i.koblikov@mail.ru"
            //};

            List<ContactData> oldContacts = app.Contacts.GetContactList();

            app.Contacts.Create(contact);
            oldContacts.Add(contact);

            List<ContactData> newContacts = app.Contacts.GetContactList();
            oldContacts.Sort();
            newContacts.Sort();
            Assert.AreEqual(oldContacts, newContacts);

            //app.Auth.Logout();
        }

        //[Test]
        ////заполнить только поля для ФИО
        //public void ContactCreationTest_OnlyFIOFields()
        //{
        //    ContactData contact = new ContactData("Maxim", "Bokov")
        //    {
        //        Middlename = "N."
        //    };

        //    List<ContactData> oldContacts = app.Contacts.GetContactList();

        //    app.Contacts.Create(contact);

        //    List<ContactData> newContacts = app.Contacts.GetContactList();
        //    oldContacts.Add(contact);
        //    oldContacts.Sort();
        //    newContacts.Sort();
        //    Assert.AreEqual(oldContacts, newContacts);

        //    //app.Auth.Logout();
        //}

        //[Test]
        ////все поля пустые
        //public void ContactCreationTest_EmptyFields()
        //{
        //    ContactData contact = new ContactData("", "");

        //    List<ContactData> oldContacts = app.Contacts.GetContactList();

        //    app.Contacts.Create(contact);

        //    List<ContactData> newContacts = app.Contacts.GetContactList();
        //    oldContacts.Add(contact);
        //    oldContacts.Sort();
        //    newContacts.Sort();
        //    Assert.AreEqual(oldContacts, newContacts);

        //    //app.Auth.Logout();
        //}
    }
}

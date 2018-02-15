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
        [Test]
        //заполнить все поля
        public void ContactCreationTest_AllFields()
        {
            ContactData contact = new ContactData("Ilya", "Koblikov");
            contact.Middlename = "A.";
            contact.Nickname = "ikoblikov";
            contact.Title = "Test Manager";
            contact.Company = "VSK";
            contact.Address = "Volgograd, Lenina av., 54";
            contact.Home = "88442051430";
            contact.Mobile = "89173256927";
            contact.Work = "88442281940";
            contact.Email = "ikoblikov@yandex.ru";
            contact.Email2 = "Koblikov@vsk.ru";

            List<ContactData> oldContacts = app.Contacts.GetContactList();

            app.Contacts.Create(contact);

            List<ContactData> newContacts = app.Contacts.GetContactList();
            oldContacts.Add(contact);
            oldContacts.Sort();
            newContacts.Sort();
            Assert.AreEqual(oldContacts, newContacts);

            //app.Auth.Logout();
        }

        [Test]
        //заполнить только поля для ФИО
        public void ContactCreationTest_OnlyFIOFields()
        {
            ContactData contact = new ContactData("Maxim", "Bokov");
            contact.Middlename = "N.";

            List<ContactData> oldContacts = app.Contacts.GetContactList();

            app.Contacts.Create(contact);

            List<ContactData> newContacts = app.Contacts.GetContactList();
            oldContacts.Add(contact);
            oldContacts.Sort();
            newContacts.Sort();
            Assert.AreEqual(oldContacts, newContacts);

            //app.Auth.Logout();
        }
        [Test]
        //все поля пустые
        public void ContactCreationTest_EmptyFields()
        {
            ContactData contact = new ContactData("", "");

            List<ContactData> oldContacts = app.Contacts.GetContactList();

            app.Contacts.Create(contact);

            List<ContactData> newContacts = app.Contacts.GetContactList();
            oldContacts.Add(contact);
            oldContacts.Sort();
            newContacts.Sort();
            Assert.AreEqual(oldContacts, newContacts);

            //app.Auth.Logout();
        }
    }
}

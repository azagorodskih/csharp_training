using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactCreationTests : TestBase
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

            app.Contacts.Create(contact);

            //app.Auth.Logout();
        }

        [Test]
        //заполнить только поля для ФИО
        public void ContactCreationTest_OnlyFIOFields()
        {
            ContactData contact = new ContactData("Maxim", "Bokov");
            contact.Middlename = "N.";

            app.Contacts.Create(contact);

            //app.Auth.Logout();
        }
        [Test]
        //все поля пустые
        public void ContactCreationTest_EmptyFields()
        {
            ContactData contact = new ContactData("", "");

            app.Contacts.Create(contact);

            //app.Auth.Logout();
        }
    }
}

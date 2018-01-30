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
        public void ContactCreationTest()
        {
            ContactData contact = new ContactData("Ilya", "Koblikov");
            contact.Middlename = "A.";
            contact.Nickname = "ikoblikov";
            contact.Title = "Test Manager";
            contact.Company = "VSK";

            app.Contacts.Create(contact);

            app.Auth.Logout();
        }

        [Test]
        public void OnlyFIOContactCreationTest()
        {
            ContactData contact = new ContactData("Maxim", "Bokov");
            contact.Middlename = "N.";

            app.Contacts.Create(contact);

            app.Auth.Logout();
        }
    }
}

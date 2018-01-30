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
            navigationHelper.OpenHomePage();
            authHelper.Login(new AccountData("admin", "secret"));
            contactHelper.InitNewContactCreation();

            ContactData contact = new ContactData("Roman", "Dumtsev");
            contact.Middlename = "S.";
            contact.Nickname = "r.dumtsev";
            contact.Title = "Technical Specialist";
            contact.Company = "EMS";
            contactHelper.FillContactForm(contact);

            contactHelper.SubmitContactCreation();
            authHelper.Logout();
        }
    }
}

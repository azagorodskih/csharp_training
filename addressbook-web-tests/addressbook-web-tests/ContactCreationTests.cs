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
            OpenHomePage();
            Login(new AccountData("admin", "secret"));
            InitNewContactCreation();

            ContactData contact = new ContactData("Roman", "Dumtsev");
            contact.Middlename = "S.";
            contact.Nickname = "r.dumtsev";
            contact.Title = "Technical Specialist";
            contact.Company = "EMS";
            FillContactForm(contact);

            SubmitContactCreation();
            Logout();
        }
    }
}

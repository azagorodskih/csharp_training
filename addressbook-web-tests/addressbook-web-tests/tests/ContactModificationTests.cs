using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactModificationTests : TestBase
    {
        [Test]
        //замена старых значений новыми, вызвано из списка контактов
        public void ContactModificationTest_NewData()
        {
            ContactData contact = new ContactData("Yuliya", "Blinova");
            contact.Middlename = "S.";
            contact.Nickname = "Y.Blinova";
            contact.Title = "Sale Specialist";
            contact.Company = "EMS";
            contact.Address = "Volgograd, Seriynaya st., 5";
            contact.Home = "88442111111";
            contact.Mobile = "89099999999";
            contact.Work = "88442555555";
            contact.Email = "y.blinova@mail.ru";
            contact.Email2 = "Blinova@ems.ru";

            app.Contacts.ModifyFromList(1, contact);
        }

        [Test]
        //замена старых значений пустыми, вызвано из карточки контакта
        public void ContactModificationTest_EmptyData()
        {
            ContactData contact = new ContactData("", "");

            app.Contacts.ModifyFromCard(2, contact);
        }

        [Test]
        //добавить один контакт в группу, вызвано из списка
        public void ContactModificationTest_AddToGroupOne()
        {
            int[] index = new int[] { 1 };

            app.Contacts.AddSelectedContactsToGroup(index, "nnn");
        }

        [Test]
        //добавить несколько контактов в группу, вызвано из списка
        public void ContactModificationTest_AddToGroupSeveral()
        {
            int[] index = new int[] { 2, 3 };

            app.Contacts.AddSelectedContactsToGroup(index, "mmm");
        }

        [Test]
        //добавить все контакты в группу, вызвано из списка
        public void ContactModificationTest_AddToGroupAll()
        {
            app.Contacts.AddAllContactsToGroup("aaa");
        }
    }
}

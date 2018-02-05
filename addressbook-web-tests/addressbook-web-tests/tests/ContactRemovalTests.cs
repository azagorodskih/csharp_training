using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactRemovalTests : TestBase
    {
        [Test]
        //удалить контакт, вызвано из карточки контакта
        public void ContactRemovalTest_RemoveFromCard()
        {
            app.Contacts.RemoveContactFromCard(1);
            //app.Auth.Logout();
        }

        [Test]
        //удалить один контакт, вызвано из списка контакта
        public void ContactRemovalTest_RemoveOne()
        {
            int[] index = new int[] { 2 };

            app.Contacts.RemoveSelectedContactsFromList(index);
            //app.Auth.Logout();
        }

        [Test]
        //удалить несколько контактов, вызвано из списка контакта
        public void ContactRemovalTest_RemoveSeveral()
        {
            int[] index = new int[] { 1, 2 };

            app.Contacts.RemoveSelectedContactsFromList(index);
        }

        [Test]
        //удалить все контакты, вызвано из списка
        public void ContactRemovalTest_RemoveAll()
        {
            app.Contacts.RemoveAllContactsFromList();
            //app.Auth.Logout();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactRemovalTests : AuthTestBase
    {
        [Test]
        //удалить контакт, вызвано из карточки контакта
        public void ContactRemovalTest_RemoveFromCard()
        {
            int index = 10;

            List<ContactData> oldContacts = app.Contacts.GetContactList();

            if (oldContacts.Count > 0)
            {
                if (! app.Contacts.IsContactPresent(index))
                {
                    index = 0;
                }
                app.Contacts.RemoveContactFromCard(index);
                oldContacts.RemoveAt(index);
            }
            else
            {
                app.Contacts.Create(new ContactData("", ""));
                app.Contacts.RemoveContactFromCard(0);
            }                       

            List<ContactData> newContacts = app.Contacts.GetContactList();
            oldContacts.Sort();
            newContacts.Sort();
            Assert.AreEqual(oldContacts, newContacts);

            //app.Auth.Logout();
        }

        [Test]
        //удалить один контакт, вызвано из списка контакта
        public void ContactRemovalTest_RemoveOne()
        {
            List<int> Index = new List<int>();
            Index.Add(10);

            List<ContactData> oldContacts = app.Contacts.GetContactList();

            if (oldContacts.Count > 0)
            {
                if (!app.Contacts.IsContactPresent(Index[0]))
                {
                    Index[0] = 0;
                }
                app.Contacts.RemoveSelectedContactsFromList(Index);
                oldContacts.RemoveAt(Index[0]);
            }
            else
            {
                app.Contacts.Create(new ContactData("", ""));
                Index[0] = 0;
                app.Contacts.RemoveSelectedContactsFromList(Index);
            }

            List<ContactData> newContacts = app.Contacts.GetContactList();
            oldContacts.Sort();
            newContacts.Sort();
            Assert.AreEqual(oldContacts, newContacts);

            //app.Auth.Logout();
        }

        [Test]
        //удалить несколько контактов, вызвано из списка контакта
        public void ContactRemovalTest_RemoveSeveral()
        {
            List<int> Index = new List<int>();
            Index.Add(3);
            Index.Add(4);
            List<int> correctIndex = new List<int>();

            List<ContactData> oldContacts_Before = app.Contacts.GetContactList();
            List<ContactData> oldContacts_After = new List<ContactData>();
            
            if (oldContacts_Before.Count > 0)
            {
                foreach (int i in Index)
                {
                    if (app.Contacts.IsContactPresent(i))
                    {
                        correctIndex.Add(i);
                    }
                }
                if (correctIndex.Count == 0)
                {
                    correctIndex.Add(0);
                }

                app.Contacts.RemoveSelectedContactsFromList(correctIndex);

                for (int i = 0; i < oldContacts_Before.Count; i++)
                {
                    if (!correctIndex.Contains(i))
                    {
                        oldContacts_After.Add(oldContacts_Before[i]);
                    }
                }
            }
            else
            {
                app.Contacts.Create(new ContactData("", ""));
                correctIndex.Add(0);
                app.Contacts.RemoveSelectedContactsFromList(correctIndex);
            }                       

            List<ContactData> newContacts = app.Contacts.GetContactList();
            oldContacts_After.Sort();
            newContacts.Sort();
            Assert.AreEqual(oldContacts_After, newContacts);
        }

        [Test]
        //удалить все контакты, вызвано из списка
        public void ContactRemovalTest_RemoveAll()
        {
            List<ContactData> oldContacts = app.Contacts.GetContactList();

            if (oldContacts.Count == 0)
            {
                app.Contacts.Create(new ContactData("", ""));
            }

            app.Contacts.RemoveAllContactsFromList();
            oldContacts.Clear();

            List<ContactData> newContacts = app.Contacts.GetContactList();
            oldContacts.Sort();
            newContacts.Sort();
            Assert.AreEqual(oldContacts, newContacts);

            //app.Auth.Logout();
        }
    }
}

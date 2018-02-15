using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactModificationTests : AuthTestBase
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
            int index = 10; //отсчет от 0

            List<ContactData> oldContacts = app.Contacts.GetContactList();

            if (oldContacts.Count > 0)
            {
                if (! app.Contacts.IsContactPresent(index))
                {
                    index = 0;
                }
                app.Contacts.ModifyFromList(index, contact);
                oldContacts[index].Firstname = contact.Firstname;
                oldContacts[index].Lastname = contact.Lastname;
            }
            else
            {
                app.Contacts.Create(contact);
                oldContacts.Add(contact);
            }                     

            List<ContactData> newContacts = app.Contacts.GetContactList();
            oldContacts.Sort();
            newContacts.Sort();
            Assert.AreEqual(oldContacts, newContacts);

            //app.Auth.Logout();
        }

        [Test]
        //замена старых значений пустыми, вызвано из карточки контакта
        public void ContactModificationTest_EmptyData()
        {
            ContactData contact = new ContactData("", "");
            int index = 10; //отсчет от 0

            List<ContactData> oldContacts = app.Contacts.GetContactList();

            if (oldContacts.Count > 0)
            {
                if (!app.Contacts.IsContactPresent(index))
                {
                    index = 0;
                }
                app.Contacts.ModifyFromList(index, contact);
                oldContacts[index].Firstname = contact.Firstname;
                oldContacts[index].Lastname = contact.Lastname;
            }
            else
            {
                app.Contacts.Create(contact);
                oldContacts.Add(contact);
            }

            List<ContactData> newContacts = app.Contacts.GetContactList();
            oldContacts.Sort();
            newContacts.Sort();
            Assert.AreEqual(oldContacts, newContacts);

            //app.Auth.Logout();
        }

        [Test]
        //добавить один контакт в группу, вызвано из списка
        public void ContactModificationTest_AddToGroupOne()
        {
            List<int> Index = new List<int>();
            Index.Add(1);
            string groupName = "www";

            List<ContactData> oldContacts = app.Contacts.GetContactList();
            //List<ContactData> oldGroupContent = app.Contacts.GetGroupContent(groupName);

            if (oldContacts.Count > 0)
            {
                if (! app.Contacts.IsContactPresent(Index[0]))
                {
                    Index[0] = 0;
                }
            }
            else
            {
                ContactData contact = new ContactData("firstName", "lastName");
                app.Contacts.Create(contact);
                oldContacts.Add(contact);
                Index[0] = 0;
            }

            app.Contacts.AddSelectedContactsToGroup(Index, groupName);
            //if (!oldGroupContent.Contains(oldContacts[Index[0]]))
            //{
                //oldGroupContent.Add(oldContacts[Index[0]]);
            //}

            List<ContactData> newContacts = app.Contacts.GetContactList();
            //List<ContactData> newGroupContent = app.Contacts.GetGroupContent(groupName);
            oldContacts.Sort();
            newContacts.Sort();
            //oldGroupContent.Sort();
            //newGroupContent.Sort();
            Assert.AreEqual(oldContacts, newContacts);
            //Assert.AreEqual(oldGroupContent, newGroupContent);
            
            //app.Auth.Logout();
        }

        [Test]
        //добавить несколько контактов в группу, вызвано из списка
        public void ContactModificationTest_AddToGroupSeveral()
        {
            List<int> Index = new List<int>();
            Index.Add(8);
            Index.Add(9);
            Index.Add(10);
            string groupName = "www";
            List<int> correctIndex = new List<int>();

            List<ContactData> oldContacts = app.Contacts.GetContactList();
            //List<ContactData> oldGroupContent = app.Contacts.GetGroupContent(groupName);

            if (oldContacts.Count > 0)
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
            }
            else
            {
                ContactData contact = new ContactData("firstName", "lastName");
                app.Contacts.Create(contact);
                oldContacts.Add(contact);
                correctIndex.Add(0);
            }

            app.Contacts.AddSelectedContactsToGroup(correctIndex, groupName);
            //oldGroupContent.Add(oldContacts[Index[0]]);

            List<ContactData> newContacts = app.Contacts.GetContactList();
            //List<ContactData> newGroupContent = app.Contacts.GetGroupContent(groupName);
            oldContacts.Sort();
            newContacts.Sort();
            //oldGroupContent.Sort();
            //newGroupContent.Sort();
            Assert.AreEqual(oldContacts, newContacts);
            //Assert.AreEqual(oldGroupContent, newGroupContent);

            //app.Auth.Logout();
        }

        [Test]
        //добавить все контакты в группу, вызвано из списка
        public void ContactModificationTest_AddToGroupAll()
        {
            string groupName = "www";

            List<ContactData> oldContacts = app.Contacts.GetContactList();
            //List<ContactData> oldGroupContent = app.Contacts.GetGroupContent(groupName);

            if (oldContacts.Count == 0)
            {
                ContactData contact = new ContactData("firstName", "lastName");
                app.Contacts.Create(contact);
                oldContacts.Add(contact);
            }

            app.Contacts.AddAllContactsToGroup(groupName);
            //foreach (ContactData contact in oldContacts)
            //{
            //    if (!oldGroupContent.Contains(contact))
            //    {
            //        oldGroupContent.Add(contact);
            //    }
            //}

            List<ContactData> newContacts = app.Contacts.GetContactList();
            List<ContactData> newGroupContent = app.Contacts.GetGroupContent(groupName);
            oldContacts.Sort();
            newContacts.Sort();
            //oldGroupContent.Sort();
            newGroupContent.Sort();
            Assert.AreEqual(oldContacts, newContacts);
            Assert.AreEqual(oldContacts, newGroupContent);

            
            //app.Auth.Logout();
        }
    }
}

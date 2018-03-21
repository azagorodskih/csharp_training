using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class AddingContactToGroupTests : AuthTestBase
    {
        [Test]
        //добавить один контакт в группу
        public void TestAddingContactToGroup_AddOne()
        {
            GroupData group = GroupData.GetAll()[0];
            List<ContactData> oldList = group.GetContacts();
            List<ContactData> contact = new List<ContactData>();
            IEnumerable<ContactData> exceptResult = ContactData.GetAll().Except(group.GetContacts());
            if (exceptResult.Count() != 0)
            {
                contact.Add(ContactData.GetAll().Except(group.GetContacts()).First());

                app.Contacts.AddSelectedContactsToGroup(contact, group);
                oldList.Add(contact[0]);

                List<ContactData> newList = group.GetContacts();
                oldList.Sort();
                newList.Sort();
                Assert.AreEqual(oldList, newList);
            }
            else
            {
                Console.Out.Write("Группа " + group.Name + " (id=" + group.Id + ") уже содержит все существующие контакты");
            }
        }

        [Test]
        //добавить несколько контактов в группу
        public void TestAddingContactToGroup_AddSeveral()
        {
            GroupData group = GroupData.GetAll()[0];
            List<ContactData> oldList = group.GetContacts();
            List<ContactData> contacts = new List<ContactData>();
            IEnumerable<ContactData> exceptResult = ContactData.GetAll().Except(group.GetContacts());
            if (exceptResult.Count() != 0)
            {
                contacts.Add(ContactData.GetAll().Except(group.GetContacts()).First());
                if (exceptResult.Count() != 1)
                {
                    contacts.Add(ContactData.GetAll().Except(group.GetContacts()).Last());
                }

                app.Contacts.AddSelectedContactsToGroup(contacts, group);
                foreach (ContactData c in contacts)
                {
                    oldList.Add(c);
                }

                List<ContactData> newList = group.GetContacts();
                oldList.Sort();
                newList.Sort();
                Assert.AreEqual(oldList, newList);
            }
            else
            {
                Console.Out.Write("Группа " + group.Name + " (id=" + group.Id + ") уже содержит все существующие контакты");
            }
        }

        [Test]
        //добавить все контакты в группу
        public void TestAddingContactToGroup_AddAll()
        {
            GroupData group = GroupData.GetAll()[0];
            List<ContactData> oldList = group.GetContacts();
            IEnumerable<ContactData> exceptResult = ContactData.GetAll().Except(group.GetContacts());
            if (exceptResult.Count() != 0)
            {
                app.Contacts.AddAllContactsToGroup(group.Name);
                oldList = ContactData.GetAll();

                List<ContactData> newList = group.GetContacts();
                oldList.Sort();
                newList.Sort();
                Assert.AreEqual(oldList, newList);
            }
            else
            {
                Console.Out.Write("Группа " + group.Name + " (id=" + group.Id + ") уже содержит все существующие контакты");
            }
        }
    }
}

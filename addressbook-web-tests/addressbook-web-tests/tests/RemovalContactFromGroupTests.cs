using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class RemovalContactFromGroupTests : AuthTestBase
    {
        [Test]
        //удалить один контакт из группы
        public void TestRemovalContactFromGroup_RemoveOne()
        {
            GroupData group = GroupData.GetAll()[0];
            List<ContactData> oldList = group.GetContacts();

            if (oldList.Count() != 0)
            {
                List<ContactData> toBeRemoved = new List<ContactData>();
                toBeRemoved.Add(oldList[0]);

                app.Contacts.RemoveSelectedContactsFromGroup(toBeRemoved, group);
                oldList.RemoveAt(0);

                List<ContactData> newList = group.GetContacts();
                oldList.Sort();
                newList.Sort();
                Assert.AreEqual(oldList, newList);
            }
            else
            {
                Console.Out.Write("Группа " + group.Name + " (id=" + group.Id + ") не содержит ни одного контакта");
            }
        }

        [Test]
        //удалить несколько контактов из группы
        public void TestRemovalContactFromGroup_RemoveSeveral()
        {
            GroupData group = GroupData.GetAll()[0];
            List<ContactData> oldList = group.GetContacts();

            if (oldList.Count() != 0)
            {
                List<ContactData> toBeRemoved = new List<ContactData>();
                toBeRemoved.Add(oldList[0]);
                if (oldList.Count() != 1)
                {
                    toBeRemoved.Add(oldList[oldList.Count - 1]);
                }

                app.Contacts.RemoveSelectedContactsFromGroup(toBeRemoved, group);
                if (oldList.Count() != 1)
                {
                    oldList.RemoveAt(oldList.Count - 1);
                }
                oldList.RemoveAt(0);

                List<ContactData> newList = group.GetContacts();
                oldList.Sort();
                newList.Sort();
                Assert.AreEqual(oldList, newList);
            }
            else
            {
                Console.Out.Write("Группа " + group.Name + " (id=" + group.Id + ") не содержит ни одного контакта");
            }
        }

        [Test]
        //удалить все контакты из группы
        public void TestRemovalContactFromGroup_RemoveAll()
        {
            GroupData group = GroupData.GetAll()[0];
            List<ContactData> oldList = group.GetContacts();

            if (oldList.Count() != 0)
            {
                app.Contacts.RemoveAllContactsFromGroup(group.Name);
                oldList.Clear();

                List<ContactData> newList = group.GetContacts();
                oldList.Sort();
                newList.Sort();
                Assert.AreEqual(oldList, newList);
                /*так как в результате этого теста должны удалиться все контакты из группы,
                то убедимся, что оба списка пусты*/
                Assert.AreEqual(0, oldList.Count);
                Assert.AreEqual(0, newList.Count);
            }
            else
            {
                Console.Out.Write("Группа " + group.Name + " (id=" + group.Id + ") не содержит ни одного контакта");
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactRemovalTests : ContactTestBase
    {
        [Test]
        //удалить контакт, вызвано из карточки контакта
        public void ContactRemovalTest_RemoveFromCard()
        {
            int index = 0; //отсчет от 0; для упрощения проверки теста удалению будет подвергаться первый контакт

            //List<ContactData> oldContacts = app.Contacts.GetContactList();
            List<ContactData> oldContacts = ContactData.GetAll();

            if (oldContacts.Count == 0)
            {
                app.Contacts.Create(new ContactData("firstName", "lastName"));
                //oldContacts.Add(new ContactData("firstName", "lastName"));
                oldContacts = ContactData.GetAll();  //app.Contacts.GetContactList(); //чтобы также узнать идентификатор созданного контакта
            }

            //app.Contacts.RemoveContactFromCard(index);
            ContactData toBeRemoved = oldContacts[index];
            app.Contacts.RemoveContactFromCard(toBeRemoved);
            oldContacts.RemoveAt(index);

            //List<ContactData> newContacts = app.Contacts.GetContactList();
            List<ContactData> newContacts = ContactData.GetAll();
            oldContacts.Sort();
            newContacts.Sort();
            Assert.AreEqual(oldContacts, newContacts);

            foreach (ContactData contact in newContacts)
            {
                Assert.AreNotEqual(contact.Id, toBeRemoved.Id);
            }

            //app.Auth.Logout();
        }

        [Test]
        //удалить один контакт, вызвано из списка контакта
        public void ContactRemovalTest_RemoveOne()
        {
            List<int> Index = new List<int>();
            Index.Add(0); //отсчет от 0; для упрощения проверки теста удалению будет подвергаться первый контакт

            //List<ContactData> oldContacts = app.Contacts.GetContactList();
            List<ContactData> oldContacts = ContactData.GetAll();

            if (oldContacts.Count == 0)
            {
                app.Contacts.Create(new ContactData("firstName", "lastName"));
                //oldContacts.Add(new ContactData("firstName", "lastName"));
                oldContacts = ContactData.GetAll(); //app.Contacts.GetContactList(); //чтобы также узнать идентификатор созданного контакта
            }

            //app.Contacts.RemoveSelectedContactsFromList(Index);
            List<ContactData> toBeRemoved = new List<ContactData>();
            toBeRemoved.Add(oldContacts[Index[0]]);
            app.Contacts.RemoveSelectedContactsFromList(toBeRemoved);
            oldContacts.RemoveAt(Index[0]);

            //List<ContactData> newContacts = app.Contacts.GetContactList();
            List<ContactData> newContacts = ContactData.GetAll();
            oldContacts.Sort();
            newContacts.Sort();
            Assert.AreEqual(oldContacts, newContacts);

            foreach (ContactData contact in newContacts)
            {
                Assert.AreNotEqual(contact.Id, toBeRemoved[0].Id);
            }

            //app.Auth.Logout();
        }

        [Test]
        //удалить несколько контактов, вызвано из списка контакта
        public void ContactRemovalTest_RemoveSeveral()
        {
            List<int> Index = new List<int>();
            Index.Add(2);
            Index.Add(1);
            Index.Add(3);

            List<ContactData> oldContacts_Before = new List<ContactData>(); //список контактов до удаления
            List<ContactData> oldContacts_After = new List<ContactData>(); //список контактов после удаления

            oldContacts_Before = ContactData.GetAll();

            int contactCount = oldContacts_Before.Count;
            foreach (int i in Index)
            {
                if (!app.Contacts.IsContactPresent(i))
                {
                    do
                    {
                        app.Contacts.Create(new ContactData("firstName" + i, "lastName" + i));
                        //oldContacts_Before.Add(new ContactData("firstName" + i, "lastName" + i));
                        contactCount++;
                    }
                    while ((contactCount - 1) != i);
                    /*oldContacts_Before.Sort(); /*сортировка сделана потому, что после добавления нового контакта 
                                        они автоматически сортируются по фамилии (видно в браузере),
                                        и в дальнейшем после удаления списки oldContacts и newContacts могут разойтись из-за этой особенности*/
                }
            }
            oldContacts_Before = ContactData.GetAll(); //app.Contacts.GetContactList();

            //app.Contacts.RemoveSelectedContactsFromList(Index);
            List<ContactData> toBeRemoved = new List<ContactData>();
            foreach (int i in Index)
            {
                toBeRemoved.Add(oldContacts_Before[i]);
            }
            app.Contacts.RemoveSelectedContactsFromList(toBeRemoved);

            /*После использования RemoveAt в списке происходит сдвиг элементов.
            Поэтому чтобы правильно сформировать в oldContacts список оставшихся после удаления контактов,
            перепишем в новый список те контакты, которые не запрашивались для удаления*/
            for (int i = 0; i < oldContacts_Before.Count; i++)
            {
                if (!Index.Contains(i))
                {
                    oldContacts_After.Add(oldContacts_Before[i]);
                }
            }

            //List<ContactData> newContacts = app.Contacts.GetContactList();
            List<ContactData> newContacts = ContactData.GetAll();
            oldContacts_After.Sort();
            newContacts.Sort();
            Assert.AreEqual(oldContacts_After, newContacts);

            for (int i = 0; i < newContacts.Count; i++)
            {
                Assert.AreEqual(newContacts[i].Id, oldContacts_After[i].Id);
            }
        }

        [Test]
        //удалить все контакты, вызвано из списка
        public void ContactRemovalTest_RemoveAll()
        {
            //List<ContactData> oldContacts = app.Contacts.GetContactList();
            List<ContactData> oldContacts = ContactData.GetAll();

            if (oldContacts.Count == 0)
            {
                app.Contacts.Create(new ContactData("firstName", "lastName"));
                //oldContacts.Add(new ContactData("firstName", "lastName"));
                oldContacts = ContactData.GetAll(); //app.Contacts.GetContactList();
            }

            app.Contacts.RemoveAllContactsFromList();
            oldContacts.Clear();

            //List<ContactData> newContacts = app.Contacts.GetContactList();
            List<ContactData> newContacts = ContactData.GetAll();
            oldContacts.Sort();
            newContacts.Sort();
            Assert.AreEqual(oldContacts, newContacts);
            /*так как в результате этого теста должны удалиться все контакты,
             то сравнивать идентификаторы не будем; убедимся, что оба списка пусты*/
            Assert.AreEqual(0, oldContacts.Count);
            Assert.AreEqual(0, newContacts.Count);

            //app.Auth.Logout();
        }
    }
}

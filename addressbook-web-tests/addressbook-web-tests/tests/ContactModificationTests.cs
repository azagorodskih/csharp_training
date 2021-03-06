﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactModificationTests : ContactTestBase
    {
        [Test]
        //замена старых значений новыми, вызвано из списка контактов
        public void ContactModificationTest_NewData()
        {
            ContactData newData = new ContactData("Alex", "Blinov");
            newData.Middlename = "Y.";
            newData.Nickname = "A.Blinov";
            newData.Title = "Sale Specialist";
            newData.Company = "EMS";
            newData.Address = "Volgograd, Seriynaya st., 5";
            newData.Home = "88442111111";
            newData.Mobile = "89099999999";
            newData.Work = "88442555555";
            newData.Email = "a.blinov@mail.ru";
            newData.Email2 = "Blinov@ems.ru";
            int index = 0; //отсчет от 0; для упрощения проверки теста модификации будет подвергаться первый контакт

            //List<ContactData> oldContacts = app.Contacts.GetContactList();
            List<ContactData> oldContacts = ContactData.GetAll();

            if (oldContacts.Count == 0)
            {
                app.Contacts.Create(new ContactData("firstName", "lastName"));
                //oldContacts.Add(new ContactData("firstName", "lastName"));
                oldContacts = ContactData.GetAll(); //app.Contacts.GetContactList(); //чтобы также узнать идентификатор созданного контакта
            }

            //app.Contacts.ModifyFromList(index, contact);
            ContactData toBeModified = oldContacts[index];
            app.Contacts.ModifyFromList(toBeModified, newData);
            oldContacts[index].Firstname = newData.Firstname;
            oldContacts[index].Lastname = newData.Lastname;

            //List<ContactData> newContacts = app.Contacts.GetContactList();
            List<ContactData> newContacts = ContactData.GetAll();
            oldContacts.Sort();
            newContacts.Sort();
            Assert.AreEqual(oldContacts, newContacts);

            foreach (ContactData contact in newContacts)
            {
                if (contact.Id == toBeModified.Id)
                {
                    Assert.AreEqual(newData.Firstname, contact.Firstname);
                    Assert.AreEqual(newData.Lastname, contact.Lastname);
                }
            }

            //app.Auth.Logout();
        }

        [Test]
        //замена старых значений пустыми, вызвано из карточки контакта
        public void ContactModificationTest_EmptyData()
        {
            ContactData newData = new ContactData("", "");
            int index = 0; //отсчет от 0; для упрощения проверки теста модификации будет подвергаться первый контакт

            //List<ContactData> oldContacts = app.Contacts.GetContactList();
            List<ContactData> oldContacts = ContactData.GetAll();

            if (oldContacts.Count == 0)
            {
                app.Contacts.Create(new ContactData("firstName", "lastName"));
                //oldContacts.Add(new ContactData("firstName", "lastName"));
                oldContacts = ContactData.GetAll(); //app.Contacts.GetContactList(); //чтобы также узнать идентификатор созданного контакта
            }

            //app.Contacts.ModifyFromCard(index, contact);
            ContactData toBeModified = oldContacts[index];
            app.Contacts.ModifyFromCard(toBeModified, newData);
            oldContacts[index].Firstname = newData.Firstname;
            oldContacts[index].Lastname = newData.Lastname;

            //List<ContactData> newContacts = app.Contacts.GetContactList();
            List<ContactData> newContacts = ContactData.GetAll();
            oldContacts.Sort();
            newContacts.Sort();
            Assert.AreEqual(oldContacts, newContacts);

            foreach (ContactData contact in newContacts)
            {
                if (contact.Id == toBeModified.Id)
                {
                    Assert.AreEqual(newData.Firstname, contact.Firstname);
                    Assert.AreEqual(newData.Lastname, contact.Lastname);
                }
            }

            //app.Auth.Logout();
        }

        //[Test]
        ////добавить один контакт в группу, вызвано из списка
        //public void ContactModificationTest_AddToGroupOne()
        //{
        //    List<int> Index = new List<int>();
        //    Index.Add(0); //отсчет от 0; для упрощения проверки теста модификации будет подвергаться первый контакт
        //    string groupName = "www";

        //    List<ContactData> oldContacts = app.Contacts.GetContactList();
        //    List<ContactData> oldGroupContent = app.Contacts.GetGroupContent(groupName);

        //    if (oldContacts.Count == 0)
        //    {
        //        app.Contacts.Create(new ContactData("firstName", "lastName"));
        //        //oldContacts.Add(new ContactData("firstName", "lastName"));
        //        oldContacts = app.Contacts.GetContactList(); //чтобы также узнать идентификатор созданного контакта
        //    }

        //    app.Contacts.AddSelectedContactsToGroup(Index, groupName);

        //    /*Определяем, в группу добавлен тот же самый контакт или нет?
        //     Из-за неуникальности имен контактов проверку сразу ведем по идентификатору*/
        //    bool isExist = false;
        //    foreach (ContactData contact in oldGroupContent)
        //    {
        //        if (contact.Id == oldContacts[Index[0]].Id)
        //        {
        //            isExist = true;
        //            break;
        //        }
        //    }
        //    if (!isExist)
        //    {
        //        oldGroupContent.Add(oldContacts[Index[0]]);
        //    }

        //    List<ContactData> newContacts = app.Contacts.GetContactList();
        //    List<ContactData> newGroupContent = app.Contacts.GetGroupContent(groupName);
        //    oldContacts.Sort();
        //    newContacts.Sort();
        //    oldGroupContent.Sort();
        //    newGroupContent.Sort();

        //    //проверяем, что добавление контакта в группу не повлияло на общий список контактов
        //    Assert.AreEqual(oldContacts, newContacts);
        //    /*Проверяем, что в списках действительно содержатся одни и те же элементы*/
        //    for (int i = 0; i < newContacts.Count; i++)
        //    {
        //        Assert.AreEqual(newContacts[i].Id, oldContacts[i].Id);
        //    }

        //    Assert.AreEqual(oldGroupContent, newGroupContent);
        //    for (int i = 0; i < newGroupContent.Count; i++)
        //    {
        //        Assert.AreEqual(newGroupContent[i].Id, oldGroupContent[i].Id);
        //    }

        //    //app.Auth.Logout();
        //}

        //[Test]
        ////добавить несколько контактов в группу, вызвано из списка
        //public void ContactModificationTest_AddToGroupSeveral()
        //{
        //    List<int> Index = new List<int>();
        //    Index.Add(2);
        //    Index.Add(1);
        //    Index.Add(3);
        //    string groupName = "www";

        //    List<ContactData> oldContacts = app.Contacts.GetContactList();
        //    List<ContactData> oldGroupContent = app.Contacts.GetGroupContent(groupName);

        //    int contactCount = oldContacts.Count;
        //    foreach (int i in Index)
        //    {
        //        if (!app.Contacts.IsContactPresent(i))
        //        {
        //            do
        //            {
        //                app.Contacts.Create(new ContactData("firstName" + i, "lastName" + i));
        //                //oldContacts.Add(new ContactData("firstName" + i, "lastName" + i));
        //                contactCount++;
        //            }
        //            while ((contactCount - 1) != i);
        //            /*oldContacts.Sort(); /*сортировка сделана потому, что после добавления нового контакта 
        //                                они автоматически сортируются по фамилии (видно в браузере),
        //                                и в дальнейшем после модификации списки oldContacts и newContacts могут разойтись из-за этой особенности*/
        //        }
        //    }
        //    oldContacts = app.Contacts.GetContactList(); //чтобы также узнать идентификаторы созданных контактов

        //    app.Contacts.AddSelectedContactsToGroup(Index, groupName);

        //    /*Определяем, в группу добавлены те же самые контакты или нет?
        //     Из-за неуникальности имен контактов проверку сразу ведем по идентификатору*/
        //    bool isExist = false;
        //    Index.Sort();/*сортировка сделана потому, что после добавления нового контакта 
        //                   они автоматически сортируются по фамилии (видно в браузере),
        //                   и в дальнейшем список oldGroupContent может быть некорректно отсортирован,
        //                   если в списке Index индексы были заданы не по порядку*/
        //    foreach (int i in Index)
        //    {
        //        foreach (ContactData contact in oldGroupContent)
        //        {
        //            if (contact.Id == oldContacts[i].Id)
        //            {
        //                isExist = true;
        //                break;
        //            }
        //        }
        //        if (!isExist)
        //        {
        //            oldGroupContent.Add(oldContacts[i]);
        //        }
        //    }

        //    List<ContactData> newContacts = app.Contacts.GetContactList();
        //    List<ContactData> newGroupContent = app.Contacts.GetGroupContent(groupName);
        //    oldContacts.Sort();
        //    newContacts.Sort();
        //    oldGroupContent.Sort();
        //    newGroupContent.Sort();

        //    //проверяем, что добавление контакта в группу не повлияло на общий список контактов
        //    Assert.AreEqual(oldContacts, newContacts);
        //    /*Проверяем, что в списках действительно содержатся одни и те же элементы*/
        //    for (int i = 0; i < newContacts.Count; i++)
        //    {
        //        Assert.AreEqual(newContacts[i].Id, oldContacts[i].Id);
        //    }

        //    Assert.AreEqual(oldGroupContent, newGroupContent);
        //    for (int i = 0; i < newGroupContent.Count; i++)
        //    {
        //        Assert.AreEqual(newGroupContent[i].Id, oldGroupContent[i].Id);
        //    }

        //    //app.Auth.Logout();
        //}

        //[Test]
        ////добавить все контакты в группу, вызвано из списка
        //public void ContactModificationTest_AddToGroupAll()
        //{
        //    string groupName = "www";

        //    List<ContactData> oldContacts = app.Contacts.GetContactList();
        //    //List<ContactData> oldGroupContent = app.Contacts.GetGroupContent(groupName);

        //    if (oldContacts.Count == 0)
        //    {
        //        app.Contacts.Create(new ContactData("firstName", "lastName"));
        //        //oldContacts.Add(new ContactData("firstName", "lastName"));
        //        oldContacts = app.Contacts.GetContactList(); //чтобы также узнать идентификаторы созданных контактов
        //    }

        //    app.Contacts.AddAllContactsToGroup(groupName);

        //    List<ContactData> newContacts = app.Contacts.GetContactList();
        //    List<ContactData> newGroupContent = app.Contacts.GetGroupContent(groupName);
        //    oldContacts.Sort();
        //    newContacts.Sort();
        //    //oldGroupContent.Sort();
        //    newGroupContent.Sort();

        //    //проверяем, что добавление контакта в группу не повлияло на общий список контактов
        //    Assert.AreEqual(oldContacts, newContacts);
        //    /*Проверяем, что в списках действительно содержатся одни и те же элементы*/
        //    for (int i = 0; i < newContacts.Count; i++)
        //    {
        //        Assert.AreEqual(newContacts[i].Id, oldContacts[i].Id);
        //    }

        //    Assert.AreEqual(oldContacts, newGroupContent);
        //    for (int i = 0; i < newGroupContent.Count; i++)
        //    {
        //        Assert.AreEqual(newGroupContent[i].Id, oldContacts[i].Id);
        //    }

        //    //app.Auth.Logout();
        //}
    }
}

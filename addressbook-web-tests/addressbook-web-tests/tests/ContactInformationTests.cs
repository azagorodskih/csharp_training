using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactInformationTests : AuthTestBase
    {
        [Test]
        /*Для некоторого отдельно взятого контакта информация на главной странице 
         соответствует информации, представленной в форме редактирования контакта */
        public void ContactInformationTest_ListAndForm()
        {
            int index = 0; //отсчет от 0; для упрощения проверки теста модификации будет подвергаться первый контакт

            ContactData fromList = app.Contacts.GetContactInfoFromList(index);
            ContactData fromForm = app.Contacts.GetContactInfoFromForm(index);

            Assert.AreEqual(fromList, fromForm);
            Assert.AreEqual(fromList.Address, fromForm.Address);
            Assert.AreEqual(fromList.AllEmails, fromForm.AllEmails);
            Assert.AreEqual(fromList.AllPhones, fromForm.AllPhones);
        }

        //[Test]
        ///*Для некоторого отдельно взятого контакта информация на странице просмотра свойств контакта 
        // соответствует информации, представленной в форме редактирования контакта */
        //public void ContactInformationTest_ListAndCard()
        //{
        //    int index = 0; //отсчет от 0; для упрощения проверки теста модификации будет подвергаться первый контакт

        //    ContactData fromList = app.Contacts.GetContactInfoFromList(index);
        //    ContactData fromCard = app.Contacts.GetContactInfoFromCard(index);

        //    Assert.AreEqual(fromList, fromCard);
        //    Assert.AreEqual(fromList.Address, fromCard.Address);
        //    Assert.AreEqual(fromList.AllEmails, fromCard.AllEmails);
        //    Assert.AreEqual(fromList.AllPhones, fromCard.AllPhones);
        //}
    }
}

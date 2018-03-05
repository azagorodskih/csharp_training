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

            /*так как для вывода в таблицу на главной странице лишние пробелы вначале и конце имени, фамилии и адреса убираются,
             то полученные из формы имя, фамилию и адрес мы также очистим от пробелов в начале и конце*/
            fromForm.Firstname = fromForm.Firstname.Trim();
            fromForm.Lastname = fromForm.Lastname.Trim();
            fromForm.Address = fromForm.CleanUpMultiline(fromForm.Address).Trim();

            Assert.AreEqual(fromList, fromForm);
            Assert.AreEqual(fromList.Address, fromForm.Address);
            Assert.AreEqual(fromList.AllEmails, fromForm.AllEmails);
            Assert.AreEqual(fromList.AllPhones, fromForm.AllPhones);
        }

        [Test]
        /*Для некоторого отдельно взятого контакта информация на странице просмотра свойств контакта 
         соответствует информации, представленной в форме редактирования контакта */
        public void ContactInformationTest_ListAndCard()
        {
            int index = 0; //отсчет от 0; для упрощения проверки теста модификации будет подвергаться первый контакт
            
            ContactData fromCard = app.Contacts.GetContactInfoFromCard(index);
            ContactData fromForm = app.Contacts.GetContactInfoFromForm(index);

            Assert.AreEqual(fromCard.FIO, fromForm.FIO);
            Assert.AreEqual(fromCard.Nickname, fromForm.Nickname.Trim());
            Assert.AreEqual(fromCard.Title, fromForm.Title.Trim());
            Assert.AreEqual(fromCard.Company, fromForm.Company.Trim());
            Assert.AreEqual(fromCard.Address, fromForm.CleanUpMultiline(fromForm.Address).Trim());
            Assert.AreEqual(fromCard.Home, "H: " + fromForm.Home.Trim());
            Assert.AreEqual(fromCard.Mobile, "M: " + fromForm.Mobile.Trim());
            Assert.AreEqual(fromCard.Work, "W: " + fromForm.Work.Trim());
            Assert.AreEqual(fromCard.Fax, "F: " + fromForm.Fax.Trim());
            Assert.AreEqual(fromCard.Email, fromForm.Email.Trim());
            Assert.AreEqual(fromCard.Email2, fromForm.Email2.Trim());
            Assert.AreEqual(fromCard.Email3, fromForm.Email3.Trim());
            Assert.AreEqual(fromCard.Homepage, "Homepage: " +  fromForm.Homepage.Trim());
            Assert.AreEqual(fromCard.Birthday, "Birthday " + fromForm.Birthday);
            Assert.AreEqual(fromCard.Anniversary, "Anniversary " + fromForm.Anniversary);
            Assert.AreEqual(fromCard.secAddress, fromForm.secAddress.Trim());
            Assert.AreEqual(fromCard.secHome, "P: " + fromForm.secHome.Trim());
            Assert.AreEqual(fromCard.Notes, fromForm.Notes.Trim());
        }
    }
}

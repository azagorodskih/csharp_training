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
            
            string fromCard = app.Contacts.GetContactInfoFromCard(index);
            ContactData fromForm = app.Contacts.GetContactInfoFromForm(index);

            string likeInCard = "";
            //ФИО
            if (fromForm.Firstname != null & fromForm.Firstname != "")
            {
                likeInCard = fromForm.Firstname.Trim() + " ";
            }
            if (fromForm.Middlename != null & fromForm.Middlename != "")
            {
                likeInCard = likeInCard + fromForm.Middlename.Trim() + " ";
            }
            if (fromForm.Lastname != null & fromForm.Lastname != "")
            {
                likeInCard = likeInCard + fromForm.Lastname.Trim();
            }
            likeInCard = likeInCard.Trim();
            //Адрес
            if (fromForm.Address != null & fromForm.Address != "")
            {
                if (likeInCard != "")
                {
                    likeInCard = likeInCard + "\r\n";
                }
                likeInCard = likeInCard + fromForm.CleanUpMultiline(fromForm.Address).Trim();
            }
            //Телефоны
            if (fromForm.Home != null & fromForm.Home != "")
            {
                if(likeInCard != "")
                {
                    likeInCard = likeInCard + "\r\n" + "\r\n";
                }
                likeInCard = likeInCard + "H: " + fromForm.Home.Trim();
            }
            if (fromForm.Mobile != null & fromForm.Mobile != "")
            {
                if (likeInCard != "")
                {
                    if (fromForm.Home == null || fromForm.Home == "")
                    {
                        likeInCard = likeInCard + "\r\n" + "\r\n";
                    }
                    else
                    {
                        likeInCard = likeInCard + "\r\n";
                    }
                }
                likeInCard = likeInCard + "M: " + fromForm.Mobile.Trim();
            }
            if (fromForm.Work != null & fromForm.Work != "")
            {
                if (likeInCard != "")
                {
                    if ((fromForm.Home == null || fromForm.Home == "") & (fromForm.Mobile == null || fromForm.Mobile == ""))
                    {
                        likeInCard = likeInCard + "\r\n" + "\r\n";
                    }
                    else
                    {
                        likeInCard = likeInCard + "\r\n";
                    }
                }
                likeInCard = likeInCard + "W: " + fromForm.Work.Trim();
            }
            //Электронная почта
            if (fromForm.Email != null & fromForm.Email != "")
            {
                if (likeInCard != "")
                {
                    likeInCard = likeInCard + "\r\n" + "\r\n";
                }
                likeInCard = likeInCard + fromForm.Email.Trim();
            }
            if (fromForm.Email2 != null & fromForm.Email2 != "")
            {
                if (likeInCard != "")
                {
                    if (fromForm.Email == null || fromForm.Email == "")
                    {
                        likeInCard = likeInCard + "\r\n" + "\r\n";
                    }
                    else
                    {
                        likeInCard = likeInCard + "\r\n";
                    }
                }
                likeInCard = likeInCard + fromForm.Email2.Trim();
            }
            if (fromForm.Email3 != null & fromForm.Email3 != "")
            {
                if (likeInCard != "")
                {
                    if ((fromForm.Email == null || fromForm.Email == "") & (fromForm.Email2 == null || fromForm.Email2 == ""))
                    {
                        likeInCard = likeInCard + "\r\n" + "\r\n";
                    }
                    else
                    {
                        likeInCard = likeInCard + "\r\n";
                    }
                }
                likeInCard = likeInCard + fromForm.Email3.Trim();
            }

            Assert.AreEqual(fromCard, likeInCard);

            //Assert.AreEqual(fromCard.FIO, fromForm.FIO);
            //Assert.AreEqual(fromCard.Nickname, fromForm.Nickname.Trim());
            //Assert.AreEqual(fromCard.Title, fromForm.Title.Trim());
            //Assert.AreEqual(fromCard.Company, fromForm.Company.Trim());
            //Assert.AreEqual(fromCard.Address, fromForm.CleanUpMultiline(fromForm.Address).Trim());
            //Assert.AreEqual(fromCard.Home, "H: " + fromForm.Home.Trim());
            //Assert.AreEqual(fromCard.Mobile, "M: " + fromForm.Mobile.Trim());
            //Assert.AreEqual(fromCard.Work, "W: " + fromForm.Work.Trim());
            //Assert.AreEqual(fromCard.Fax, "F: " + fromForm.Fax.Trim());
            //Assert.AreEqual(fromCard.Email, fromForm.Email.Trim());
            //Assert.AreEqual(fromCard.Email2, fromForm.Email2.Trim());
            //Assert.AreEqual(fromCard.Email3, fromForm.Email3.Trim());
            //Assert.AreEqual(fromCard.Homepage, "Homepage: " +  fromForm.Homepage.Trim());
            //Assert.AreEqual(fromCard.Birthday, "Birthday " + fromForm.Birthday);
            //Assert.AreEqual(fromCard.Anniversary, "Anniversary " + fromForm.Anniversary);
            //Assert.AreEqual(fromCard.secAddress, fromForm.secAddress.Trim());
            //Assert.AreEqual(fromCard.secHome, "P: " + fromForm.secHome.Trim());
            //Assert.AreEqual(fromCard.Notes, fromForm.Notes.Trim());
        }
    }
}

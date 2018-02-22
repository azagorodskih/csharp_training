using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace WebAddressbookTests
{
    public class ContactHelper : HelperBase
    {
        public ContactHelper(ApplicationManager manager) 
            : base(manager)
        {
        }

        public ContactHelper Create(ContactData contact)
        {
            manager.Navigator.OpenHomePage();
            InitNewContactCreation();
            FillContactForm(contact);
            SubmitContactCreation();
            manager.Navigator.OpenHomePage();
            return this;
        }

        public ContactHelper ModifyFromList(int index, ContactData newData)
        {
            manager.Navigator.OpenHomePage();
            InitModifyContactFromList(index);
            Modify(newData);
            return this;
        }
                
        public ContactHelper ModifyFromCard(int index, ContactData newData)
        {
            manager.Navigator.OpenHomePage();
            OpenContactCardForModify(index);
            Modify(newData);
            return this;
        }

        public ContactHelper AddSelectedContactsToGroup(List<int> index, string groupName)
        {
            manager.Navigator.OpenHomePage();
            foreach (int i in index)
            {
                SelectContact(i);
            }
            AddToGroup(groupName);
            return this;
        }

        public ContactHelper AddAllContactsToGroup(string groupName)
        {
            manager.Navigator.OpenHomePage();
            SelectAllContacts();
            AddToGroup(groupName);
            return this;
        }  

        public ContactHelper RemoveContactFromCard(int index)
        {
            manager.Navigator.OpenHomePage();
            RemoveFromCard(index);
            //ReturnToHomePage();
            return this;
        }
                
        public ContactHelper RemoveSelectedContactsFromList(List<int> index)
        {
            manager.Navigator.OpenHomePage();
            foreach (int i in index)
            {
                SelectContact(i);
            }
            RemoveFromList();
            return this;
        }
                
        public ContactHelper RemoveAllContactsFromList()
        {
            manager.Navigator.OpenHomePage();
            SelectAllContacts();
            RemoveFromList();
            return this;
        }

        public ContactData GetContactInfoFromForm(int index)
        {
            manager.Navigator.OpenHomePage();
            InitModifyContactFromList(index);

            string firstName = driver.FindElement(By.Name("firstname")).GetAttribute("value");
            string lastName = driver.FindElement(By.Name("lastname")).GetAttribute("value");
            string address = driver.FindElement(By.Name("address")).GetAttribute("value");

            string email = driver.FindElement(By.Name("email")).GetAttribute("value");
            string email2 = driver.FindElement(By.Name("email2")).GetAttribute("value");
            string email3 = driver.FindElement(By.Name("email3")).GetAttribute("value");

            string homePhone = driver.FindElement(By.Name("home")).GetAttribute("value");
            string mobilePhone = driver.FindElement(By.Name("mobile")).GetAttribute("value");
            string workPhone = driver.FindElement(By.Name("work")).GetAttribute("value");

            return new ContactData(firstName, lastName)
            {
                Address = address,
                Email = email,
                Email2 = email2,
                Email3 = email3,
                Home = homePhone,
                Mobile = mobilePhone,
                Work = workPhone
            };
        }

        //public ContactData GetContactInfoFromCard(ContactData contact)
        //{
            
        //}

        public ContactData GetContactInfoFromList(int index)
        {
            manager.Navigator.OpenHomePage();
            IList<IWebElement> cells = driver.FindElements(By.Name("entry"))[index]
                .FindElements(By.TagName("td"));

            string firstName = cells[2].Text;
            string lastName = cells[1].Text;
            string address = cells[3].Text;
            string emails = cells[4].Text;
            string phones = cells[5].Text;

            return new ContactData(firstName, lastName)
            {
                Address = address,
                AllEmails = emails,
                AllPhones = phones
            };
        }

        public ContactHelper InitNewContactCreation()
        {
            driver.FindElement(By.LinkText("add new")).Click();
            return this;
        }

        public ContactHelper FillContactForm(ContactData contact)
        {
            Type(By.Name("firstname"), contact.Firstname);
            Type(By.Name("middlename"), contact.Middlename);
            Type(By.Name("lastname"), contact.Lastname);
            Type(By.Name("nickname"), contact.Nickname);
            Type(By.Name("title"), contact.Title);
            Type(By.Name("company"), contact.Company);
            Type(By.Name("address"), contact.Address);
            Type(By.Name("home"), contact.Home);
            Type(By.Name("mobile"), contact.Mobile);
            Type(By.Name("work"), contact.Work);
            Type(By.Name("email"), contact.Email);
            Type(By.Name("email2"), contact.Email2);
            return this;
        }

        public ContactHelper SubmitContactCreation()
        {
            driver.FindElement(By.Name("submit")).Click();
            contactCash = null;
            return this;
        }

        //public ContactHelper ReturnToHomePage()
        //{
        //    driver.FindElement(By.LinkText("home page")).Click();
        //    return this;
        //}

        public ContactHelper SubmitContactModification()
        {
            driver.FindElement(By.Name("update")).Click();
            contactCash = null;
            groupcontentCash = null;
            return this;
        }

        public ContactHelper OpenContactCard(int index)
        {
            driver.FindElement(By.XPath("(//img[@alt='Details'])[" + (index + 1) + "]")).Click();
            return this;
        }

        public ContactHelper InitContactModification()
        {
            driver.FindElement(By.Name("modifiy")).Click();
            return this;
        }

        public ContactHelper OpenContactCardForModify(int index)
        {
            OpenContactCard(index);
            InitContactModification();
            return this;
        }

        public ContactHelper InitModifyContactFromList(int index)
        {
            driver.FindElement(By.XPath("(//img[@alt='Edit'])[" + (index + 1) + "]")).Click();
            return this;
        }

        public ContactHelper GoToGroupPage(string groupName)
        {
            driver.FindElement(By.LinkText("group page \"" + groupName + "\"")).Click();
            return this;
        }

        public ContactHelper SubmitAddToGroup()
        {
            driver.FindElement(By.Name("add")).Click();
            groupcontentCash = null;
            return this;
        }

        public ContactHelper SelectGroupForAdd(string groupName)
        {
            new SelectElement(driver.FindElement(By.Name("to_group"))).SelectByText(groupName);
            return this;
        }

        public ContactHelper ShowGroupContent(string groupName)
        {
            new SelectElement(driver.FindElement(By.Name("group"))).SelectByText(groupName);
            return this;
        }

        public ContactHelper SelectContact(int index)
        {
            driver.FindElement(By.XPath("(//input[@name='selected[]'])[" + (index + 1) + "]")).Click();
            return this;
        }

        public ContactHelper SelectAllContacts()
        {
            driver.FindElement(By.Id("MassCB")).Click();
            return this;
        }

        public ContactHelper SubmitContactRemoval()
        {
            try
            {
                //driver.SwitchTo().Alert() = доступ к появившемуся диалоговому окну
                //Accept() = нажатие на кнопку ОК
                driver.SwitchTo().Alert().Accept();
                contactCash = null;
                groupcontentCash = null;
            }
            catch (NoAlertPresentException)
            {
                //исключение: диалоговое окно не появилось, ничего не делаем
            }
            
            return this;
        }

        /*так как кнопка для удаления контакта из карточки и из списка имеет одинаковое название,
        то для удаления используется один метод, но необходимость очистки кеша определяется по флагу fromCard.
        При удалении контакта из карточки, он удаляется сразу, поэтому кеш также можно сразу очистить.
        При удалении контакта из списка, свои действия необходимо подтвердить; 
        очистка кеша в этом случае происходит в методе SubmitContactRemoval()*/
        public ContactHelper RemoveContact(bool fromCard)
        {
            driver.FindElement(By.XPath("//input[@value='Delete']")).Click();
            if (fromCard)
            {
                contactCash = null;
                groupcontentCash = null;
            }
            return this;
        }

        public ContactHelper Modify(ContactData newData)
        {
            FillContactForm(newData);
            SubmitContactModification();
            //ReturnToHomePage();
            manager.Navigator.OpenHomePage();
            return this;
        }

        public ContactHelper AddToGroup(string groupName)
        {
            SelectGroupForAdd(groupName);
            SubmitAddToGroup();
            GoToGroupPage(groupName);
            return this;
        }

        public ContactHelper RemoveFromList()
        {
            RemoveContact(false);
            SubmitContactRemoval();
            //ReturnToHomePage();
            return this;
        }

        public bool IsContactPresent(int index)
        {
            return IsElementPresent(By.XPath("(//img[@alt='Edit'])[" + (index + 1) + "]"));
        }

        public void RemoveFromCard(int index)
        {
            OpenContactCardForModify(index);
            RemoveContact(true);
        }

        private List<ContactData> contactCash = null;
        private List<ContactData> groupcontentCash = null;

        public List<ContactData> GetContactList()
        {
            if (contactCash == null)
            {
                contactCash = new List<ContactData>();
                manager.Navigator.OpenHomePage();
                contactCash = GetList(contactCash);
            }

            return new List<ContactData>(contactCash);
        }

        public List<ContactData> GetList(List<ContactData> contacts)
        {
            ICollection<IWebElement> elements = driver.FindElements(By.CssSelector("tr[name=entry]"));
            foreach (IWebElement element in elements)
            {
                //string firstName = element.FindElement(By.XPath("./td[3]")).Text;
                //string lastName = element.FindElement(By.XPath("./td[2]")).Text;
                //contacts.Add(new ContactData(firstName, lastName));

                contacts.Add(new ContactData(element.FindElement(By.XPath("./td[3]")).Text,
                    element.FindElement(By.XPath("./td[2]")).Text) {
                    Id = element.FindElement(By.TagName("input")).GetAttribute("value") });
            }
            return contacts;
        }

        public List<ContactData> GetGroupContent(string groupName)
        {
            if (groupcontentCash == null)
            {
                groupcontentCash = new List<ContactData>();
                manager.Navigator.OpenHomePage();
                ShowGroupContent(groupName);
                groupcontentCash = GetList(groupcontentCash);
                manager.Navigator.OpenHomePage();
            }

            return new List<ContactData>(groupcontentCash);
        }
    }
}

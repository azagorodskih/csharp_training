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
            }
            catch (NoAlertPresentException)
            {
                //исключение: диалоговое окно не появилось, ничего не делаем
            }
            
            return this;
        }

        public ContactHelper RemoveContact()
        {
            driver.FindElement(By.XPath("//input[@value='Delete']")).Click();
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
            RemoveContact();
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
            RemoveContact();
        }

        public List<ContactData> GetContactList()
        {
            List<ContactData> contacts = new List<ContactData>();

            manager.Navigator.OpenHomePage();
            contacts = GetList(contacts);
            return contacts;
        }

        public List<ContactData> GetGroupContent(string groupName)
        {
            List<ContactData> contacts = new List<ContactData>();

            manager.Navigator.OpenHomePage();
            ShowGroupContent(groupName);
            contacts = GetList(contacts);
            manager.Navigator.OpenHomePage();
            return contacts;
        }

        public List<ContactData> GetList(List<ContactData> contacts)
        {
            ICollection<IWebElement> elements = driver.FindElements(By.CssSelector("tr[name=entry]"));
            int i = 2;
            foreach (IWebElement element in elements)
            {
                string firstName = element.FindElement(By.XPath("//table[@id='maintable']/tbody/tr[" + i + "]/td[3]")).Text;
                string lastName = element.FindElement(By.XPath("//table[@id='maintable']/tbody/tr[" + i + "]/td[2]")).Text;
                contacts.Add(new ContactData(firstName, lastName));
                i++;
            }
            return contacts;
        }
    }
}

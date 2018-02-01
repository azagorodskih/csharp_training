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
            InitNewContactCreation();
            FillContactForm(contact);
            SubmitContactCreation();
            return this;
        }

        public ContactHelper Modify(ContactData newData)
        {
            FillContactForm(newData);
            SubmitContactModification();
            ReturnToHomePage();
            return this;
        }
                
        public ContactHelper ModifyFromList(int index, ContactData newData)
        {
            ModifyContactFromList(index);
            Modify(newData);
            return this;
        }

        public ContactHelper ModifyFromCard(int index, ContactData newData)
        {
            OpenContactCardForModify(index);
            Modify(newData);
            return this;
        }
                
        public ContactHelper AddToGroup(string groupName)
        {
            SelectGroupForAdd(groupName);
            SubmitAddToGroup();
            GoToGroupPage(groupName);
            return this;
        }

        public ContactHelper AddSelectedContactsToGroup(int[] index, string groupName)
        {
            foreach (int i in index)
            {
                SelectContact(i);
            }
            AddToGroup(groupName);
            return this;
        }

        public ContactHelper AddAllContactsToGroup(string groupName)
        {
            SelectAllContacts();
            AddToGroup(groupName);
            return this;
        }  

        public ContactHelper RemoveContactFromCard(int index)
        {
            OpenContactCardForModify(index);
            RemoveContact();
            //ReturnToHomePage();
            return this;
        }
        
        public ContactHelper RemoveFromList()
        {
            RemoveContact();
            SubmitContactRemoval();
            //ReturnToHomePage();
            return this;
        }

        public ContactHelper RemoveSelectedContactsFromList(int[] index)
        {
            foreach (int i in index)
            {
                SelectContact(i);
            }
            RemoveFromList();
            return this;
        }
                
        public ContactHelper RemoveAllContactsFromList()
        {
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
            driver.FindElement(By.Name("firstname")).Clear();
            driver.FindElement(By.Name("firstname")).SendKeys(contact.Firstname);
            driver.FindElement(By.Name("middlename")).Clear();
            driver.FindElement(By.Name("middlename")).SendKeys(contact.Middlename);
            driver.FindElement(By.Name("lastname")).Clear();
            driver.FindElement(By.Name("lastname")).SendKeys(contact.Lastname);
            driver.FindElement(By.Name("nickname")).Clear();
            driver.FindElement(By.Name("nickname")).SendKeys(contact.Nickname);
            driver.FindElement(By.Name("title")).Clear();
            driver.FindElement(By.Name("title")).SendKeys(contact.Title);
            driver.FindElement(By.Name("company")).Clear();
            driver.FindElement(By.Name("company")).SendKeys(contact.Company);
            driver.FindElement(By.Name("address")).Clear();
            driver.FindElement(By.Name("address")).SendKeys(contact.Address);
            driver.FindElement(By.Name("home")).Clear();
            driver.FindElement(By.Name("home")).SendKeys(contact.Home);
            driver.FindElement(By.Name("mobile")).Clear();
            driver.FindElement(By.Name("mobile")).SendKeys(contact.Mobile);
            driver.FindElement(By.Name("work")).Clear();
            driver.FindElement(By.Name("work")).SendKeys(contact.Work);
            driver.FindElement(By.Name("email")).Clear();
            driver.FindElement(By.Name("email")).SendKeys(contact.Email);
            driver.FindElement(By.Name("email2")).Clear();
            driver.FindElement(By.Name("email2")).SendKeys(contact.Email2);
            return this;
        }

        public ContactHelper SubmitContactCreation()
        {
            driver.FindElement(By.Name("submit")).Click();
            return this;
        }

        public ContactHelper ReturnToHomePage()
        {
            driver.FindElement(By.LinkText("home page")).Click();
            return this;
        }

        public ContactHelper SubmitContactModification()
        {
            driver.FindElement(By.Name("update")).Click();
            return this;
        }

        public ContactHelper OpenContactCard(int index)
        {
            driver.FindElement(By.XPath("(//img[@alt='Details'])[" + index + "]")).Click();
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

        public ContactHelper ModifyContactFromList(int index)
        {
            driver.FindElement(By.XPath("(//img[@alt='Edit'])[" + index + "]")).Click();
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
            new SelectElement(driver.FindElement(By.Name("to_group"))).SelectByText(groupName); ;
            return this;
        }

        public ContactHelper SelectContact(int index)
        {
            driver.FindElement(By.XPath("(//input[@name='selected[]'])[" + index + "]")).Click();
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
    }
}

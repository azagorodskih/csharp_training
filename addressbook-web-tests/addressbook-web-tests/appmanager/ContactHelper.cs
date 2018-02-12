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
                                
        public ContactHelper ModifyContactFromList(int index, ContactData newData)
        {
            manager.Navigator.OpenHomePage();
            if (IsContactPresent(index))
            {
                ModifyFromList(index, newData);
            }
            else if (IsContactPresent(1))
            {
                ModifyFromList(1, newData);
            }
            else
            {
                Create(newData);
            }
            return this;
        }
                
        public ContactHelper ModifyContactFromCard(int index, ContactData newData)
        {
            manager.Navigator.OpenHomePage();
            if (IsContactPresent(index))
            {
                ModifyfromCard(index, newData);
            }
            else if (IsContactPresent(1))
            {
                ModifyfromCard(1, newData);
            }
            else
            {
                Create(newData);
            }
            return this;
        }

        public ContactHelper AddSelectedContactsToGroup(int[] index, string groupName)
        {
            int totalSelected = 0;

            manager.Navigator.OpenHomePage();
            foreach (int i in index)
            {
                if (IsContactPresent(i))
                {
                    SelectContact(i);
                    totalSelected++;
                }
            }

            if (totalSelected == 0)
            {
                if (!IsContactPresent(1))
                {
                    Create(new ContactData("", ""));
                }
                SelectContact(1);
            }

            AddToGroup(groupName);
            return this;
        }

        public ContactHelper AddAllContactsToGroup(string groupName)
        {
            manager.Navigator.OpenHomePage();
            if (! IsContactPresent(1))
            {
                Create(new ContactData("", ""));
            }
            SelectAllContacts();
            AddToGroup(groupName);
            return this;
        }  

        public ContactHelper RemoveContactFromCard(int index)
        {
            manager.Navigator.OpenHomePage();
            if (IsContactPresent(index))
            {
                RemoveFromCard(index);
            }
            else
            {
                if (!IsContactPresent(1))
                {
                    Create(new ContactData("", ""));
                }
                RemoveFromCard(1);
            }
            //ReturnToHomePage();
            return this;
        }
                
        public ContactHelper RemoveSelectedContactsFromList(int[] index)
        {
            int totalSelected = 0;

            manager.Navigator.OpenHomePage();
            foreach (int i in index)
            {
                if (IsContactPresent(i))
                {
                    SelectContact(i);
                    totalSelected++;
                }
            }

            if (totalSelected == 0)
            {
                if (!IsContactPresent(1))
                {
                    Create(new ContactData("", ""));
                }
                SelectContact(1);
            }

            RemoveFromList();
            return this;
        }
                
        public ContactHelper RemoveAllContactsFromList()
        {
            manager.Navigator.OpenHomePage();
            if (! IsContactPresent(1))
            {
                Create(new ContactData("", ""));
            }
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

        public ContactHelper InitModifyContactFromList(int index)
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
            return IsElementPresent(By.XPath("(//img[@alt='Edit'])[" + index + "]"));
        }

        public void ModifyFromList(int index, ContactData newData)
        {
            InitModifyContactFromList(index);
            Modify(newData);
        }

        public void ModifyfromCard(int index, ContactData newData)
        {
            OpenContactCardForModify(index);
            Modify(newData);
        }

        public void RemoveFromCard(int index)
        {
            OpenContactCardForModify(index);
            RemoveContact();
        }
    }
}

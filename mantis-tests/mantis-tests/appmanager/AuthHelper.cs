using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace mantis_tests
{
    public class AuthHelper : HelperBase
    {
        public AuthHelper(ApplicationManager manager) : base(manager) { }

        public void Login(AccountData account)
        {
            if (IsLoggedIn())
            {
                if (IsLoggedIn(account))
                {
                    return;
                }
                Logout();
            }
            FillAuthForm("username", account.Name);
            FillAuthForm("password", account.Password);
        }

        private void FillAuthForm(string fieldName, string fieldValue)
        {
            Type(By.Name(fieldName), fieldValue);
            driver.FindElement(By.XPath("//input[@value='Войти']")).Click();
        }

        public void Logout()
        {
            if (IsLoggedIn())
            {
                driver.FindElement(By.LinkText("выход")).Click();
            }
        }

        public bool IsLoggedIn()
        {
            return IsElementPresent(By.LinkText("выход"));
        }

        public bool IsLoggedIn(AccountData account)
        {
            return IsLoggedIn()
                && GetLoggetUserName() == account.Name;
        }

        private string GetLoggetUserName()
        {
            return driver.FindElement(By.CssSelector("span.user-info")).Text;
        }
    }
}

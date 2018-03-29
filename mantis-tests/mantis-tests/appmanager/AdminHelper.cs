using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using SimpleBrowser.WebDriver;
using System.Text.RegularExpressions;

namespace mantis_tests
{
    public class AdminHelper : HelperBase
    {
        private string baseUrl;

        public AdminHelper(ApplicationManager manager, string baseUrl) : base(manager)
        {
            this.baseUrl = baseUrl;
        }

        public List<AccountData> GetAllAccounts()
        {
            List<AccountData> accounts = new List<AccountData>();

            IWebDriver driver = OpenAppAndLogin();
            driver.Url = baseUrl + "/manage_user_page.php";
            ICollection<IWebElement> elements = driver.FindElement(By.CssSelector("div.table-responsive")).FindElements(By.CssSelector("tr"));

            foreach (IWebElement element in elements)
            {
                IWebElement link = element.FindElement(By.TagName("a"));
                string username = link.Text;
                string href = link.GetAttribute("href");
                Match match = Regex.Match(href, @"\d+$");
                string id = match.Value;

                accounts.Add(new AccountData(username, "")
                {
                    Id = id
                });
            }

            return accounts;
        }

        public void DeleteAccount(AccountData account)
        {
            IWebDriver driver = OpenAppAndLogin();
            driver.Url = baseUrl + "/manage_user_edit_page.php?user_id=" + account.Id;
            driver.FindElement(By.XPath("//input[@value='Удалить учетную запись']")).Click();
            driver.FindElement(By.XPath("//input[@value='Удалить учетную запись']")).Click();
        }

        public IWebDriver OpenAppAndLogin()
        {
            IWebDriver driver = new SimpleBrowserDriver();
            driver.Url = baseUrl + "/login_page.php";
            driver.FindElement(By.Name("username")).SendKeys("administrator");
            driver.FindElement(By.XPath("//input[@value='Войти']")).Click();
            driver.FindElement(By.Name("password")).SendKeys("root");
            driver.FindElement(By.XPath("//input[@value='Войти']")).Click();
            return driver;
        }
    }
}

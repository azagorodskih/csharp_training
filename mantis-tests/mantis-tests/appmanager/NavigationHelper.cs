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
    public class NavigationHelper : HelperBase
    {
        private string baseURL;

        public NavigationHelper(ApplicationManager manager) : base(manager)
        {
            this.baseURL = manager.BaseURL;
        }

        public void OpenHomePage()
        {

            if (driver.Url == baseURL + "/login_page.php")
            {
                return;
            }
            driver.Navigate().GoToUrl(baseURL + "/login_page.php");
        }

        //перейти на страницу профиля пользователя
        public void GoToAccountPage()
        {
            if (driver.Url == baseURL + "/account_page.php"
                && IsElementPresent(By.Id("account-update-div")))
            {
                return;
            }
            manager.Driver.Url = baseURL + "/account_page.php";
        }

        //перейти на страницу управления
        public void GoToManagementPage()
        {
            if (driver.Url == baseURL + "/manage_overview_page.php"
                && IsElementPresent(By.Id("manage-overview-table")))
            {
                return;
            }

            /*Если в меню присутствует кнопка "Создать задачу", то кнопка "Управление" смещается на позицию 7 в элементе sidebar*/
            if (IsElementPresent(By.XPath("//div[@id='sidebar']/ul/li[7]")))
            {
                driver.FindElement(By.XPath("//div[@id='sidebar']/ul/li[7]/a/i")).Click();
            }
            else
            {
                driver.FindElement(By.XPath("//div[@id='sidebar']/ul/li[6]/a/i")).Click();
            }
        }

        //перейти на вкладку "Управление проектами"
        public void GoToProjectManagementMenu()
        {
            if (driver.Url == baseURL + "/manage_proj_page.php"
                && IsElementPresent(By.XPath("//input[@name='manage_proj_create_page_token']")))
            {
                return;
            }
            driver.FindElement(By.LinkText("Управление проектами")).Click();
        }

        //вернуться на вкладку "Управление проектами". При этом проверить, что создание проекта прошло успешно
        public bool ReturnToProjectManagementMenu()
        {

            if (IsElementPresent(By.CssSelector("div.alert alert-danger")))
            {
                return false;
            }
            else
            {
                if (driver.Url != baseURL + "/manage_proj_page.php")
                {
                    driver.Navigate().GoToUrl(baseURL + "/manage_proj_page.php");
                }
                return true;
            }
        }
    }
}

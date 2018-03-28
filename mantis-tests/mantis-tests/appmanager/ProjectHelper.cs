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
    public class ProjectHelper : HelperBase
    {
        public ProjectHelper(ApplicationManager manager) : base(manager) { }

        public bool Create(ProjectData project)
        {
            //manager.Navigator.GoToAccountPage();
            manager.Navigator.GoToManagementPage();
            manager.Navigator.GoToProjectManagementMenu();
            InitProjectCreation();
            FillProjectCreationForm(project);
            SubmitProjectCreation();
            return manager.Navigator.ReturnToProjectManagementMenu();
        }

        public void Remove(int index)
        {
            manager.Navigator.GoToManagementPage();
            manager.Navigator.GoToProjectManagementMenu();
            OpenProjectPage(index);
            InitProjectRemoval();
            SubmitProjectRemoval();
            manager.Navigator.ReturnToProjectManagementMenu();
        }

        public void InitProjectCreation()
        {
            driver.FindElement(By.XPath("//button[@type='submit']")).Click();
        }

        public void FillProjectCreationForm(ProjectData project)
        {
            Type(By.Name("name"), project.Name);
            Type(By.Name("description"), project.Description);
        }

        public void InitProjectRemoval()
        {
            driver.FindElement(By.XPath("//input[@value='Удалить проект']")).Click();
        }

        public void OpenProjectPage(int index)
        {
            manager.Navigator.GoToManagementPage();
            manager.Navigator.GoToProjectManagementMenu();
            IWebElement element = driver.FindElement(By.CssSelector("div.table-responsive")).FindElement(By.XPath("./table/tbody/tr[" + (index + 1) + "]"));
            element.FindElement(By.XPath("./td[1]/a")).Click();
        }

        public List<ProjectData> GetProjects()
        {
            List<ProjectData> projects = new List<ProjectData>();

            manager.Navigator.GoToManagementPage();
            manager.Navigator.GoToProjectManagementMenu();
            ICollection<IWebElement> elements = driver.FindElement(By.CssSelector("div.table-responsive")).FindElements(By.XPath("./table/tbody/tr"));

            foreach (IWebElement element in elements)
            {
                string name = element.FindElement(By.XPath("./td[1]/a")).Text;
                string description = element.FindElement(By.XPath("./td[5]")).Text;
                projects.Add(new ProjectData(name)
                {
                    Description = description
                });
            }
            return projects;
        }

        public void SubmitProjectCreation()
        {
            driver.FindElement(By.XPath("//input[@value='Добавить проект']")).Click();
        }

        public void SubmitProjectRemoval()
        {
            driver.FindElement(By.XPath("//input[@value='Удалить проект']")).Click();
        }
    }
}

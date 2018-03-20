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
    public class GroupHelper : HelperBase
    {
        public GroupHelper(ApplicationManager manager)
            : base(manager)
        {
        }

        public GroupHelper Create(GroupData group)
        {
            manager.Navigator.GoToGroupsPage();
            InitNewGroupCreation();
            FillGroupForm(group);
            SubmitGroupCreation();
            //ReturnToGroupsPage();
            manager.Navigator.GoToGroupsPage();
            return this;
        }

        public GroupHelper Modify(int index, GroupData group)
        {
            manager.Navigator.GoToGroupsPage();
            SelectGroup(index);
            InitGroupModification();
            FillGroupForm(group);
            SubmitGroupModification();
            //ReturnToGroupsPage();
            manager.Navigator.GoToGroupsPage();
            return this;
        }

        public GroupHelper Remove(GroupData group)
        {
            manager.Navigator.GoToGroupsPage();
            //foreach (int i in index)
            //{
            //    SelectGroup(i);
            //}
            SelectGroup(group.Id);
            SubmitGroupRemoval();
            manager.Navigator.GoToGroupsPage();
            return this;
        }

        public GroupHelper Remove(List<int> index)
        {
            manager.Navigator.GoToGroupsPage();
            foreach (int i in index)
            {
                SelectGroup(i);
            }
            SubmitGroupRemoval();
            //ReturnToGroupsPage();
            manager.Navigator.GoToGroupsPage();
            return this;
        }

        //public GroupHelper ModifyGroup(int index, GroupData group)
        //{
        //    manager.Navigator.GoToGroupsPage();
        //    if (IsGroupPresent(index))
        //    {
        //        Modify(index, group);
        //    }
        //    else if(IsGroupPresent(0))
        //    {
        //        Modify(0, group);
        //    }
        //    else
        //    {
        //        Create(group);
        //    }
        //    manager.Navigator.GoToGroupsPage();
        //    return this;
        //}

        //public GroupHelper RemoveGroup(int[] index)
        //{
        //    int totalSelected = 0;

        //    manager.Navigator.GoToGroupsPage();
        //    foreach (int i in index)
        //    {
        //        if (IsGroupPresent(i))
        //        {
        //            SelectGroup(i);
        //            totalSelected++;
        //        }
        //    }

        //    if (totalSelected == 0)
        //    {
        //        if (! IsGroupPresent(0))
        //        {
        //            Create(new GroupData(""));
        //        }
        //        SelectGroup(0);
        //    }

        //    Remove();
        //    //ReturnToGroupsPage();
        //    manager.Navigator.GoToGroupsPage();
        //    return this;
        //}

        public GroupHelper InitNewGroupCreation()
        {
            driver.FindElement(By.Name("new")).Click();
            return this;
        }

        public GroupHelper FillGroupForm(GroupData group)
        {
            Type(By.Name("group_name"), group.Name);
            Type(By.Name("group_header"), group.Header);
            Type(By.Name("group_footer"), group.Footer);
            return this;
        }

        public GroupHelper SubmitGroupCreation()
        {
            driver.FindElement(By.Name("submit")).Click();
            groupCash = null;
            return this;
        }

        public GroupHelper SelectGroup(int index)
        {
            driver.FindElement(By.XPath("(//input[@name='selected[]'])[" + (index + 1) + "]")).Click();
            return this;
        }

        public GroupHelper SelectGroup(String id)
        {
            driver.FindElement(By.XPath("(//input[@name='selected[]' and @value='"+ id +"'])")).Click();
            return this;
        }

        public GroupHelper SubmitGroupRemoval()
        {
            driver.FindElement(By.Name("delete")).Click();
            groupCash = null;
            return this;
        }

        //public GroupHelper ReturnToGroupsPage()
        //{
        //    driver.FindElement(By.LinkText("groups")).Click();
        //    return this;
        //}

        public GroupHelper SubmitGroupModification()
        {
            driver.FindElement(By.Name("update")).Click();
            groupCash = null;
            return this;
        }

        public GroupHelper InitGroupModification()
        {
            driver.FindElement(By.Name("edit")).Click();
            return this;
        }

        public bool IsGroupPresent(int index)
        {
            return IsElementPresent(By.XPath("(//input[@name='selected[]'])[" + (index + 1) + "]")) ;
        }

        private List<GroupData> groupCash = null;

        public List<GroupData> GetGroupList()
        {
            if (groupCash == null)
            {
                groupCash = new List<GroupData>();
                manager.Navigator.GoToGroupsPage();
                ICollection<IWebElement> elements = driver.FindElements(By.CssSelector("span.group"));
                foreach (IWebElement element in elements)
                {
                    groupCash.Add(new GroupData(element.Text) {
                        Id = element.FindElement(By.TagName("input")).GetAttribute("value") });
                }
            }

            return new List<GroupData>(groupCash);
        }

        public int GetGroupCount()
        {
            return driver.FindElements(By.CssSelector("span.group")).Count;
        }
    }
}
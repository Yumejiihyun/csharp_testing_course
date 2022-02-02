using OpenQA.Selenium;
using System;
using System.Collections.Generic;

namespace addressbooktests
{
    public class GroupHelper : HelperBase
    {
        public GroupHelper(ApplicationManager application) : base(application)
        {
        }
        public void CreateNewGroup(GroupData group)
        {
            InitNewGroup();
            FillGroupForm(group);
            SubmitGroupForm();
        }
        internal void EditGroup(int groupNumber, GroupData group)
        {
            InitGroupEdit(groupNumber);
            FillGroupForm(group);
            SubmitGroupEdit();
        }

        internal List<GroupData> GetGroupList()
        {
            List<GroupData> groupList = new List<GroupData>();
            var elements = application.driver.FindElements(By.CssSelector("span.group"));
            foreach(var element in elements)
            {
                groupList.Add(new GroupData(element.Text));
            }
            return groupList;
        }

        internal void RemoveGroup(int groupNumber)
        {
            application.driver.FindElement(By.XPath($"//div[@id='content']/form/span[{groupNumber}]/input")).Click();
            application.driver.FindElement(By.Name("delete")).Click();
        }
        private void SubmitGroupForm()
        {
            application.driver.FindElement(By.Name("submit")).Click();
        }

        private void FillGroupForm(GroupData group)
        {
            Type(By.Name("group_name"), group.Name);
            Type(By.Name("group_header"), group.Header);
            Type(By.Name("group_footer"), group.Footer);
        }

        private void SubmitGroupEdit()
        {
            application.driver.FindElement(By.Name("update")).Click();
        }

        private void InitGroupEdit(int groupNumber)
        {
            application.driver.FindElement(By.XPath($"//div[@id='content']/form/span[{groupNumber}]/input")).Click();
            application.driver.FindElement(By.Name("edit")).Click();
        }

        private void InitNewGroup()
        {
            application.driver.FindElement(By.Name("new")).Click();
        }
    }
}
using OpenQA.Selenium;
using System;

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
            application.driver.FindElement(By.Name("group_name")).Click();
            application.driver.FindElement(By.Name("group_name")).SendKeys(group.Name);
            application.driver.FindElement(By.Name("group_header")).SendKeys(group.Header);
            application.driver.FindElement(By.Name("group_footer")).SendKeys(group.Footer);
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
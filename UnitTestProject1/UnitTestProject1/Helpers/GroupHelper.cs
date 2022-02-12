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

        internal int GetGroupCount()
        {
            return application.driver.FindElements(By.CssSelector("span.group")).Count;
        }

        private List<GroupData> groupListCash = null;

        internal List<GroupData> GetGroupList()
        {
            if (groupListCash is null)
            {
                groupListCash = new List<GroupData>();
                var elements = application.driver.FindElements(By.CssSelector("span.group"));
                foreach(var element in elements)
                {
                    var id = element.FindElement(By.TagName("input")).GetAttribute("value");
                    groupListCash.Add(new GroupData(null)
                    {
                        Id = id,
                    });
                }

                string allGroupNames = application.driver.FindElement(By.CssSelector("div#content form")).Text;
                string[] parts = allGroupNames.Split('\n');
                int shift = groupListCash.Count - parts.Length;
                for (int i = 0; i < groupListCash.Count; i++)
                {
                    if (i < shift)
                    {
                        groupListCash[i].Name = "";
                    }
                    else
                    { 
                        groupListCash[i].Name = parts[i].Trim();
                    }
                }
            }
            return new List<GroupData>(groupListCash);
        }

        internal void RemoveGroup(int groupNumber)
        {
            application.driver.FindElement(By.XPath($"//div[@id='content']/form/span[{groupNumber}]/input")).Click();
            application.driver.FindElement(By.Name("delete")).Click();
            groupListCash = null;
        }
        internal void RemoveGroup(GroupData groupData)
        {
            application.driver.FindElement(By.XPath($"//input[@name='selected[]' and @value='{groupData.Id}']")).Click();
            application.driver.FindElement(By.Name("delete")).Click();
            groupListCash = null;
        }
        private void SubmitGroupForm()
        {
            application.driver.FindElement(By.Name("submit")).Click();
            groupListCash = null;
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
            groupListCash = null;
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
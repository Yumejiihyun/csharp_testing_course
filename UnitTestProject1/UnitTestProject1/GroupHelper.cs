using OpenQA.Selenium;

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

        private void InitNewGroup()
        {
            application.driver.FindElement(By.Name("new")).Click();
        }
    }
}
using OpenQA.Selenium;
using System;

namespace addressbooktests
{
    public class NavigationHelper : HelperBase
    {
        public NavigationHelper(ApplicationManager application) : base(application)
        {
        }

        public void GoToGroupsPage()
        {
            if (!(application.driver.Url == "http://localhost/addressbook/group.php" && IsElementPresent(By.Name("new"))))
            {
                application.driver.FindElement(By.LinkText("groups")).Click();
            }
        }
        public void GoToNewContact()
        {
            application.driver.FindElement(By.LinkText("add new")).Click();
        }
        public void GoToEditContact(int contactNumber)
        {
            application.driver.FindElement(By.XPath($"(//img[@alt='Edit'])[{contactNumber}]")).Click();
        }

        public void GoToHomePage()
        {
            application.driver.FindElement(By.LinkText("home")).Click();
            application.driver.Manage().Window.Size = new System.Drawing.Size(1920, 1036);
        }

        internal bool IsGroupPresent() => IsElementPresent(By.XPath($"//div[@id='content']/form/span[1]/input"));

        internal bool IsContactPresent() => IsElementPresent(By.XPath($"(//img[@alt='Edit'])[1]"));
    }
}
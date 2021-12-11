using OpenQA.Selenium;

namespace addressbooktests
{
    public class NavigationHelper : HelperBase
    {
        public NavigationHelper(ApplicationManager application) : base(application)
        {
        }

        public void GoToGroupsPage()
        {
            application.driver.FindElement(By.LinkText("groups")).Click();
        }
        public void GoToNewContact()
        {
            application.driver.FindElement(By.LinkText("add new")).Click();
        }
        public void ReturnToHomePage()
        {
            application.driver.FindElement(By.LinkText("home page")).Click();
            application.driver.Manage().Window.Size = new System.Drawing.Size(1920, 1036);
        }
    }
}
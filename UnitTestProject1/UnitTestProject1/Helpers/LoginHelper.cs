using OpenQA.Selenium;

namespace addressbooktests
{
    public class LoginHelper : HelperBase
    {
        public LoginHelper(ApplicationManager application) : base(application)
        {
        }

        public LoginHelper OpenHomePage()
        {
            application.driver.Navigate().GoToUrl("http://localhost/addressbook/");
            return this;
        }
        public LoginHelper Login(AccountData accountData)
        {
            application.driver.Manage().Window.Size = new System.Drawing.Size(1920, 1036);
            application.driver.FindElement(By.Name("user")).Click();
            application.driver.FindElement(By.Name("user")).SendKeys(accountData.Username);
            application.driver.FindElement(By.Name("pass")).SendKeys(accountData.Password);
            application.driver.FindElement(By.CssSelector("input:nth-child(7)")).Click();
            return this;
        }
    }
}
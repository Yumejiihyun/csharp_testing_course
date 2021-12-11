using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using NUnit.Framework;

namespace addressbooktests
{
    public class ApplicationManager
    {
        public IWebDriver driver;
        private AccountData admin = new AccountData("admin", "secret");

        public ApplicationManager()
        {
            driver = new EdgeDriver();
            GroupHelper = new GroupHelper(this);
            ContactHelper = new ContactHelper(this);
            LoginHelper = new LoginHelper(this);
            NavigationHelper = new NavigationHelper(this);
        }

        public GroupHelper GroupHelper { get; set; }
        public ContactHelper ContactHelper { get; set; }
        public LoginHelper LoginHelper { get; set; }
        public NavigationHelper NavigationHelper { get; set; }
        public void LoginAsAdmin() => LoginHelper.OpenHomePage().Login(admin);
        public void Close() => driver.Close();
    }
}
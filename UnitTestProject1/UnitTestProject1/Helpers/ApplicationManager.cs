using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using System.Threading;
using NUnit.Framework;

namespace addressbooktests
{
    public class ApplicationManager
    {
        public IWebDriver driver;
        private AccountData admin = new AccountData("admin", "secret");
        private static ThreadLocal<ApplicationManager> app = new ThreadLocal<ApplicationManager>();

        public ApplicationManager()
        {
            driver = new EdgeDriver();
            GroupHelper = new GroupHelper(this);
            ContactHelper = new ContactHelper(this);
            LoginHelper = new LoginHelper(this);
            NavigationHelper = new NavigationHelper(this);
        }

        ~ApplicationManager() => driver.Close();

        public static ApplicationManager GetInstance()
        {
            if (!app.IsValueCreated)
            {
                app.Value = new ApplicationManager();
                app.Value.LoginHelper.OpenHomePage();
            }
            return app.Value;
        }

        public GroupHelper GroupHelper { get; set; }
        public ContactHelper ContactHelper { get; set; }
        public LoginHelper LoginHelper { get; set; }
        public NavigationHelper NavigationHelper { get; set; }
        public void LoginAsAdmin() => LoginHelper.OpenHomePage().Login(admin);
    }
}
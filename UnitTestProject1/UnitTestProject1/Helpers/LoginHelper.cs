using OpenQA.Selenium;
using System;

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
            if(IsLoggedIn())
            {
                if(IsLoggedIn(accountData))
                {
                    return this;
                }

                LogOut();
            }
            Type(By.Name("user"), accountData.Username);
            Type(By.Name("pass"), accountData.Password);
            application.driver.FindElement(By.CssSelector("input:nth-child(7)")).Click();
            return this;
        }

        public bool IsLoggedIn()
        {
            return IsElementPresent(By.Name("logout"));
        }

        public void LogOut()
        {
            if(IsLoggedIn()) application.driver.FindElement(By.LinkText("Logout")).Click();
        }

        public bool IsLoggedIn(AccountData accountData)
        {
            return IsLoggedIn() 
                && application.driver.FindElement(By.Name("logout")).FindElement(By.TagName("b")).Text 
                == "(" + accountData.Username + ")";
        }
    }
}
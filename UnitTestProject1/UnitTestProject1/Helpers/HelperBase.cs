using OpenQA.Selenium;

namespace addressbooktests
{
    public class HelperBase
    {
        protected ApplicationManager application;

        public HelperBase(ApplicationManager application)
        {
            this.application = application;
        }

        protected void Type(By locator, string text)
        {
            if (text != null) application.driver.FindElement(locator).SendKeys(text);
        }

        public bool IsElementPresent(By by)
        {
            try
            {
                application.driver.FindElement(by);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }
    }
}
using OpenQA.Selenium;

namespace addressbooktests
{
    public class ContactHelper : HelperBase
    {

        public ContactHelper(ApplicationManager application) : base(application)
        {
        }
        public void FillContactForm(ContactData contact)
        {
            Type(By.Name("firstname"), contact.FirstName);
            Type(By.Name("middlename"), contact.MiddleName);
            Type(By.Name("lastname"), contact.LastName);
            Type(By.Name("nickname"), contact.NickName);
            Type(By.Name("title"), contact.Title);
            Type(By.Name("company"), contact.Company);
            Type(By.Name("address"), contact.Address);
            Type(By.Name("home"), contact.Home);
            Type(By.Name("mobile"), contact.Mobile);
            Type(By.Name("email"), contact.Email);
            if (contact.Bday != null) { FillBirthData(); }

            void FillBirthData()
            {
                application.driver.FindElement(By.Name("bday")).Click();
                {
                    var dropdown = application.driver.FindElement(By.Name("bday"));
                    dropdown.FindElement(By.XPath($"//option[. = {contact.Bday[0]}]")).Click();
                }
                application.driver.FindElement(By.Name("bmonth")).Click();
                {
                    var dropdown = application.driver.FindElement(By.Name("bmonth"));
                    dropdown.FindElement(By.XPath($"//option[. = '{contact.Bday[1]}']")).Click();
                }
                application.driver.FindElement(By.Name("byear")).Click();
                application.driver.FindElement(By.Name("byear")).SendKeys(contact.Bday[2]);
            }
        }
        public void ConfirmAddingNewContact()
        {
            application.driver.FindElement(By.CssSelector("input:nth-child(87)")).Click();
        }
        public void ConfirmUpdatingContact()
        {
            application.driver.FindElement(By.Name("update")).Click();
        }
        public void DeleteContactFromHomePage(int contactNumber)
        {
            application.driver.FindElement(By.XPath($"//tr[{contactNumber + 1}]/td/input")).Click();
            application.driver.FindElement(By.XPath("//input[@value='Delete']")).Click();
            application.driver.SwitchTo().Alert().Accept();
        }
        public void DeleteContactFromEditPage(int contactNumber)
        {
            application.NavigationHelper.GoToEditContact(contactNumber);
            application.driver.FindElement(By.XPath("//input[@value='Delete']")).Click();
        }
    }
}
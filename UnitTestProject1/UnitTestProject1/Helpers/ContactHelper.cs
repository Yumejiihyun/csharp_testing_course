﻿using OpenQA.Selenium;

namespace addressbooktests
{
    public class ContactHelper : HelperBase
    {

        public ContactHelper(ApplicationManager application) : base(application)
        {
        }
        public void FillContactForm(ContactData contact)
        {
            application.driver.FindElement(By.Name("firstname")).SendKeys(contact.FirstName);
            application.driver.FindElement(By.Name("middlename")).SendKeys(contact.MiddleName);
            application.driver.FindElement(By.Name("lastname")).SendKeys(contact.LastName);
            application.driver.FindElement(By.Name("nickname")).SendKeys(contact.NickName);
            application.driver.FindElement(By.Name("title")).SendKeys(contact.Title);
            application.driver.FindElement(By.Name("company")).SendKeys(contact.Company);
            application.driver.FindElement(By.Name("address")).SendKeys(contact.Address);
            application.driver.FindElement(By.Name("home")).SendKeys(contact.Home);
            application.driver.FindElement(By.Name("mobile")).SendKeys(contact.Mobile);
            application.driver.FindElement(By.Name("email")).SendKeys(contact.Email);
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
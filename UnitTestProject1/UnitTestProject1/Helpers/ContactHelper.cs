using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

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

        internal void AddContactToGroup(ContactData contact, GroupData group)
        {
            application.NavigationHelper.GoToHomePage();
            new SelectElement(application.driver.FindElement(By.Name("group"))).SelectByText("[all]");
            application.driver.FindElement(By.Id($"{contact.Id}")).Click();
            new SelectElement(application.driver.FindElement(By.Name("to_group"))).SelectByText(group.Name);
            application.driver.FindElement(By.Name("add")).Click();
            new WebDriverWait(application.driver, TimeSpan.FromSeconds(10))
                .Until(d => d.FindElements(By.CssSelector("div.msgbox")).Count > 0);
        }

        private List<ContactData> contactListCash = null;

        internal List<ContactData> GetContactList()
        {
            if (contactListCash is null)
            {
                contactListCash = new List<ContactData>();
                var elements = application.driver.FindElements(By.XPath("//tr[position()>1]"));
                contactListCash.AddRange(from element in elements
                                         let lastName = element.FindElement(By.XPath("td[2]"))
                                         let firstName = element.FindElement(By.XPath("td[3]"))
                                         select new ContactData(firstName.Text, lastName.Text));
            }
            return contactListCash;
        }

        public void ConfirmAddingNewContact()
        {
            application.driver.FindElement(By.CssSelector("input:nth-child(87)")).Click();
            contactListCash = null;
        }
        public void ConfirmUpdatingContact()
        {
            application.driver.FindElement(By.Name("update")).Click();
            contactListCash = null;
        }
        public void DeleteContactFromHomePage(int contactNumber)
        {
            application.driver.FindElement(By.XPath($"//tr[{contactNumber + 1}]/td/input")).Click();
            application.driver.FindElement(By.XPath("//input[@value='Delete']")).Click();
            application.driver.SwitchTo().Alert().Accept();
            application.NavigationHelper.GoToHomePage();
            contactListCash = null;
        }

        public void DeleteContactFromHomePage(ContactData toBeRemoved)
        {
            application.driver.FindElement(By.XPath($"//input[@id='{toBeRemoved.Id}']")).Click();
            application.driver.FindElement(By.XPath("//input[@value='Delete']")).Click();
            application.driver.SwitchTo().Alert().Accept();
            application.NavigationHelper.GoToHomePage();
            contactListCash = null;
        }

        public void DeleteContactFromEditPage(int contactNumber)
        {
            application.NavigationHelper.GoToEditContact(contactNumber);
            application.driver.FindElement(By.XPath("//input[@value='Delete']")).Click();
            application.NavigationHelper.GoToHomePage();
            contactListCash = null;
        }

        internal void DeleteContactFromEditPage(ContactData toBeRemoved)
        {
            application.NavigationHelper.GoToEditContact(toBeRemoved);
            application.driver.FindElement(By.XPath("//input[@value='Delete']")).Click();
            application.NavigationHelper.GoToHomePage();
            contactListCash = null;
        }

        internal ContactData GetContactInformationFromTable(int index)
        {
            application.NavigationHelper.GoToHomePage();

            IList<IWebElement> cells = application.driver.FindElements(By.Name("entry"))[index]
                .FindElements(By.TagName("td"));
            string lastName = cells[1].Text;
            string firstName = cells[2].Text;
            string address = cells[3].Text;
            string allEmails = cells[4].Text;
            string allPhones = cells[5].Text;
            return new ContactData(firstName, lastName)
            {
                Address = address,
                Emails = allEmails,
                Phones = allPhones,
            };
        }

        internal ContactData GetContactInformationFromEditForm(int index)
        {
            application.NavigationHelper.GoToHomePage();
            application.NavigationHelper.GoToEditContact(index + 1);

            string firstName = GetValueByName("firstname");
            string lastName = GetValueByName("lastname");
            string address = GetValueByName("address");
            string email = GetValueByName("email");
            string email2 = GetValueByName("email2");
            string email3 = GetValueByName("email3");
            string home = GetValueByName("home");
            string mobilePhone = GetValueByName("mobile");
            string workPhone = GetValueByName("work");

            application.NavigationHelper.GoToHomePage();

            return new ContactData(firstName, lastName)
            {
                Address = address,
                Email = email,
                Email2 = email2,
                Email3 = email3,
                Home = home,
                Mobile = mobilePhone,
                Work = workPhone,
            };
            string GetValueByName(string name)
            {
                return application.driver.FindElement(By.Name(name)).GetAttribute("value");
            }
        }
        internal ContactData GetGeneralContactInformationFromEditForm(int index)
        {
            application.NavigationHelper.GoToHomePage();
            application.NavigationHelper.GoToEditContact(index + 1);

            string firstName = GetValueByName("firstname");
            string middlename = GetValueByName("middlename");
            string lastName = GetValueByName("lastname");
            string nickname = GetValueByName("nickname");
            string company = GetValueByName("company");
            string title = GetValueByName("title");
            string address = GetValueByName("address");
            string email = GetValueByName("email");
            string email2 = GetValueByName("email2");
            string email3 = GetValueByName("email3");
            string home = GetValueByName("home");
            string mobilePhone = GetValueByName("mobile");
            string workPhone = GetValueByName("work");
            string[] bDay;
            try
            {
                bDay = new string[]
                {
                    application.driver.FindElements(By.XPath("//select[@name='bday']/option[@selected and @value!='-']")).FirstOrDefault().GetAttribute("value"),
                    application.driver.FindElements(By.XPath("//select[@name='bmonth']/option[@selected and @value!='-']")).FirstOrDefault().GetAttribute("value"),
                    GetValueByName("byear"),
                };
            }
            catch (NullReferenceException)
            {
                bDay = null;
            }

            application.NavigationHelper.GoToHomePage();

            return new ContactData(firstName, middlename, lastName)
            {
                NickName = nickname,
                Company = company,
                Title = title,
                Address = address,
                Email = email,
                Email2 = email2,
                Email3 = email3,
                Home = home,
                Mobile = mobilePhone,
                Work = workPhone,
                Bday = bDay,
            };
            string GetValueByName(string name)
            {
                return application.driver.FindElement(By.Name(name)).GetAttribute("value");
            }
        }

        internal string GetGeneralInformation()
        {
            string var = application.driver.FindElement(By.XPath(@"//div[@id='content']")).Text;
            return var.Replace("\r\n", string.Empty).Replace("H: ", string.Empty).Replace("M: ", string.Empty).Replace("W: ", string.Empty);
        }

        public int GetNumberOfSearchResults()
        {
            application.NavigationHelper.GoToHomePage();
            string searchText = application.driver.FindElement(By.TagName("label")).Text;
            Match m = new Regex(@"\d+").Match(searchText);
            return Int32.Parse(m.Value);
        }
    }
}
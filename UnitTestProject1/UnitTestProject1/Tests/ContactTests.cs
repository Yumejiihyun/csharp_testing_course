using NUnit.Framework;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Xml.Serialization;
using System.IO;

namespace addressbooktests
{
    [TestFixture]
    public class ContactTests : TestBase
    {
        public static IEnumerable<ContactData> RandomContactDataProvider()
        {
            List<ContactData> contacts = new List<ContactData>();
            for (int i = 0; i < 5; i++)
            {
                contacts.Add(new ContactData(GenerateRandomString(30))
                {
                    FirstName = GenerateRandomString(100),
                    MiddleName = GenerateRandomString(100),
                    LastName = GenerateRandomString(100),
                    Title = GenerateRandomString(100),
                    Company = GenerateRandomString(100),
                    Address = GenerateRandomString(100),
                    Home = GenerateRandomIntString(10000),
                    Mobile = GenerateRandomIntString(10000),
                    Email = GenerateRandomString(100),
                });
            }
            return contacts;
        }

        public static IEnumerable<GroupData> ContactDataFromXmlFile()
        {
            return (List<GroupData>)
                new XmlSerializer(typeof(List<ContactData>))
                .Deserialize(new StreamReader(@"contacts.xml"));
        }

        public static IEnumerable<ContactData> ContactDataFromJsonFile()
        {
            return JsonConvert.DeserializeObject<List<ContactData>>(
                File.ReadAllText(@"contacts.json"));
        }

        [Test, TestCaseSource("ContactDataFromJsonFile")]
        public void NewContactTest(ContactData contact)
        {
            List<ContactData> oldContacts = ContactData.GetAll();
            app.NavigationHelper.GoToNewContact();
            app.ContactHelper.FillContactForm(contact);
            app.ContactHelper.ConfirmAddingNewContact();
            app.NavigationHelper.GoToHomePage();

            oldContacts.Add(contact);
            List<ContactData> newContacts = ContactData.GetAll();
            oldContacts.Sort();
            newContacts.Sort();
            Assert.AreEqual(oldContacts, newContacts);
        }

        [Test]
        public void NewContactTest()
        {
            List<ContactData> oldContacts = ContactData.GetAll();
            app.NavigationHelper.GoToNewContact();
            ContactData vanya = CreateVanya777();
            app.ContactHelper.FillContactForm(vanya);
            app.ContactHelper.ConfirmAddingNewContact();
            app.NavigationHelper.GoToHomePage();

            oldContacts.Add(vanya);
            List<ContactData> newContacts = ContactData.GetAll();
            oldContacts.Sort();
            newContacts.Sort();
            Assert.AreEqual(oldContacts, newContacts);

            ContactData CreateVanya777()
            {
                ContactData contact = new ContactData("Vanya777")
                {
                    FirstName = "Ivan",
                    MiddleName = "Ivanovich",
                    LastName = "Ivanov",
                    Title = "Software Engineer",
                    Company = "Emerson",
                    Address = "St. Petersburg",
                    Home = "666666",
                    Mobile = "777777",
                    Email = "Ivanov@mail.ru",
                    Bday = new string[] { "7", "July", "1907" }
                };
                return contact;
            }
        }
        [Test]
        public void EditContactTest()
        {
            if(!app.NavigationHelper.IsContactPresent())
            {
                app.NavigationHelper.GoToNewContact();
                app.ContactHelper.ConfirmAddingNewContact();
                app.NavigationHelper.GoToHomePage();
            }
            List<ContactData> oldContacts = ContactData.GetAll();
            app.NavigationHelper.GoToEditContact(1);
            var editedContact = new ContactData("Barantsev");
            oldContacts[0] = editedContact;
            app.ContactHelper.FillContactForm(editedContact);
            app.ContactHelper.ConfirmUpdatingContact();
            app.NavigationHelper.GoToHomePage();

            List<ContactData> newContacts = ContactData.GetAll();
            Assert.AreEqual(oldContacts, newContacts);
        }
        [Test]
        public void RemoveContactFromHomePageTest()
        {
            if (!app.NavigationHelper.IsContactPresent())
            {
                app.NavigationHelper.GoToNewContact();
                app.ContactHelper.ConfirmAddingNewContact();
                app.NavigationHelper.GoToHomePage();
            }
            List<ContactData> oldContacts = ContactData.GetAll();
            var toBeRemoved = oldContacts[0];
            app.ContactHelper.DeleteContactFromHomePage(toBeRemoved);
            List<ContactData> newContacts = ContactData.GetAll();
            oldContacts.RemoveAt(0);
            Assert.AreEqual(oldContacts, newContacts);
        }
        [Test]
        public void RemoveContactFromEditPageTest()
        {
            if (!app.NavigationHelper.IsContactPresent())
            {
                app.NavigationHelper.GoToNewContact();
                app.ContactHelper.ConfirmAddingNewContact();
                app.NavigationHelper.GoToHomePage();
            }
            List<ContactData> oldContacts = ContactData.GetAll();
            var toBeRemoved = oldContacts[0];
            app.ContactHelper.DeleteContactFromEditPage(toBeRemoved);
            List<ContactData> newContacts = ContactData.GetAll();
            oldContacts.RemoveAt(0);
            Assert.AreEqual(oldContacts, newContacts);
        }
        [Test]
        public void ContactInformationTest()
        {
            ContactData fromTable = app.ContactHelper.GetContactInformationFromTable(0);
            ContactData fromEditForm = app.ContactHelper.GetContactInformationFromEditForm(0);
            Assert.AreEqual(fromTable, fromEditForm);
            Assert.AreEqual(fromTable.Address, fromEditForm.Address);
            Assert.AreEqual(fromTable.Phones, fromEditForm.Phones);
            Assert.AreEqual(fromTable.Emails, fromEditForm.Emails);
        }
        [Test]
        public void ContactGeneralInformationTest()
        {
            ContactData fromEditForm = app.ContactHelper.GetGeneralContactInformationFromEditForm(1);
            string generalInformationExpected = fromEditForm.ToGeneralInformation();
            app.NavigationHelper.GoToGeneralInformation(1);
            string generalInformation = app.ContactHelper.GetGeneralInformation();
            Assert.AreEqual(generalInformationExpected, generalInformation);
        }
    }
}
using NUnit.Framework;
using System.Collections.Generic;

namespace addressbooktests
{
    [TestFixture]
    public class ContactTests : TestBase
    {       
        [Test]
        public void NewContactTest()
        {
            List<ContactData> oldContacts = app.ContactHelper.GetContactList();
            app.NavigationHelper.GoToNewContact();
            ContactData vanya = CreateVanya777();
            app.ContactHelper.FillContactForm(vanya);
            app.ContactHelper.ConfirmAddingNewContact();
            app.NavigationHelper.GoToHomePage();

            oldContacts.Add(vanya);
            List<ContactData> newContacts = app.ContactHelper.GetContactList();
            oldContacts.Sort();
            newContacts.Sort();
            Assert.AreEqual(oldContacts, newContacts);

            ContactData CreateVanya777()
            {
                ContactData contact = new ContactData("Vanya777");
                contact.FirstName = "Ivan";
                contact.MiddleName = "Ivanovich";
                contact.LastName = "Ivanov";
                contact.Title = "Software Engineer";
                contact.Company = "Emerson";
                contact.Address = "St. Petersburg";
                contact.Home = "St. Petersburg";
                contact.Mobile = "777777";
                contact.Email = "Ivanov@mail.ru";
                contact.Bday = new string[] { "7", "July", "1907" };
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
            List<ContactData> oldContacts = app.ContactHelper.GetContactList();
            app.NavigationHelper.GoToEditContact(1);
            var editedContact = new ContactData("Barantsev");
            oldContacts[0] = editedContact;
            app.ContactHelper.FillContactForm(editedContact);
            app.ContactHelper.ConfirmUpdatingContact();
            app.NavigationHelper.GoToHomePage();

            List<ContactData> newContacts = app.ContactHelper.GetContactList();
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
            List<ContactData> oldContacts = app.ContactHelper.GetContactList();
            app.ContactHelper.DeleteContactFromHomePage(1);
            List<ContactData> newContacts = app.ContactHelper.GetContactList();
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
            List<ContactData> oldContacts = app.ContactHelper.GetContactList();
            app.ContactHelper.DeleteContactFromEditPage(1);
            List<ContactData> newContacts = app.ContactHelper.GetContactList();
            oldContacts.RemoveAt(0);
            Assert.AreEqual(oldContacts, newContacts);
        }
    }
}
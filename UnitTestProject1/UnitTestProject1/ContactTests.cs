using NUnit.Framework;

namespace addressbooktests
{
    [TestFixture]
    public class ContactTests : TestBase
    {       
        [Test]
        public void NewContactTest()
        {
            app.NavigationHelper.GoToNewContact();
            ContactData vanya = CreateVanya777();
            app.ContactHelper.FillContactForm(vanya);
            app.ContactHelper.ConfirmAddingNewContact();
            app.NavigationHelper.ReturnToHomePage();

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
    }
}
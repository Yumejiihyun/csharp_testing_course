using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace addressbooktests
{
    public class ContactToGroupTests : TestBase
    {
        [Test]
        public void AddingContactToGroupTest()
        {
            GroupData group = GroupData.GetAll()[0];
            List<ContactData> oldList = group.GetContacts();
            ContactData contact = ContactData.GetAll().Except(oldList).First();

            app.ContactHelper.AddContactToGroup(contact, group);

            List<ContactData> newList = group.GetContacts();
            oldList.Add(contact);
            newList.Sort();
            oldList.Sort();

            Assert.AreEqual(oldList, newList);
        }

        [Test]
        public void RemoveContactFromGroupTest()
        {
            GroupData group = GroupData.GetAll()[0];
            List<ContactData> oldList = group.GetContacts();
            if(oldList.Count == 0)
            {
                var contactToAdd = ContactData.GetAll().First();
                app.ContactHelper.AddContactToGroup(contactToAdd, group);
                oldList = group.GetContacts();
                app.NavigationHelper.GoToHomePage();
            }
            ContactData contact = oldList[0];

            app.ContactHelper.RemoveContactFromGroup(contact, group);

            List<ContactData> newList = group.GetContacts();
            oldList.RemoveAt(0);
            newList.Sort();
            oldList.Sort();

            Assert.AreEqual(oldList, newList);
        }
    }
}

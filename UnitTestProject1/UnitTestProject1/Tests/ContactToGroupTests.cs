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
            var groups = GroupData.GetAll();
            GroupData group = ContactToGroupChecks(groups, "AddingContactToGroupTest");
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
            var groups = GroupData.GetAll();
            GroupData group = ContactToGroupChecks(groups, "RemoveContactFromGroupTest");
            List<ContactData> oldList = group.GetContacts();
            if (oldList.Count == 0)
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

        private GroupData ContactToGroupChecks(List<GroupData> groups, string newGroupName)
        {
            GroupData group;
            if (!app.NavigationHelper.IsContactPresent())
            {
                app.NavigationHelper.GoToNewContact();
                app.ContactHelper.ConfirmAddingNewContact();
                app.NavigationHelper.GoToHomePage();
            }
            if (groups.Count == 0)
            {
                app.NavigationHelper.GoToGroupsPage();
                app.GroupHelper.CreateNewGroup(group = new GroupData(newGroupName));
                app.NavigationHelper.GoToHomePage();
                return group;
            }

            return groups[0];
        }
    }
}

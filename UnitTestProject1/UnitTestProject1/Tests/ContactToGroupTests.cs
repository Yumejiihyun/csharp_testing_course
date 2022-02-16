using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace addressbooktests
{
    public class ContactToGroupTests : TestBase
    {
        [Test]
        public void AddContactToGroupTest()
        {
            var groups = GroupData.GetAll();
            GroupData group = ContactsAndGroupsChecksForEmtyness(groups, "AddingContactToGroupTest");
            List<ContactData> oldList = group.GetContacts();
            ContactData contact = ContactData.GetAll().Except(oldList).FirstOrDefault();
            if (contact is null)
            {
                oldList = ContactToGroupChecks(group, app.ContactHelper.RemoveContactFromGroup);
                contact = ContactData.GetAll().Except(oldList).First();
            }

            app.ContactHelper.AddContactToGroup(contact, group);

            List<ContactData> newList = group.GetContacts();
            oldList.Add(contact);
            newList.Sort();
            oldList.Sort();

            Assert.AreEqual(oldList, newList);
        }

        private delegate void Del(ContactData contactData, GroupData groupData);

        private List<ContactData> ContactToGroupChecks(GroupData group, Del razraz)
        {
            List<ContactData> oldList;
            var contactToRemove = ContactData.GetAll().First();
            razraz(contactToRemove, group);
            oldList = group.GetContacts();
            app.NavigationHelper.GoToHomePage();
            return oldList;
        }

        [Test]
        public void RemoveContactFromGroupTest()
        {
            var groups = GroupData.GetAll();
            GroupData group = ContactsAndGroupsChecksForEmtyness(groups, "RemoveContactFromGroupTest");
            List<ContactData> oldList = group.GetContacts();
            if (oldList.Count == 0)
            {
                oldList = ContactToGroupChecks(group, app.ContactHelper.AddContactToGroup);
            }
            ContactData contact = oldList[0];

            app.ContactHelper.RemoveContactFromGroup(contact, group);

            List<ContactData> newList = group.GetContacts();
            oldList.RemoveAt(0);
            newList.Sort();
            oldList.Sort();

            Assert.AreEqual(oldList, newList);
        }

        private GroupData ContactsAndGroupsChecksForEmtyness(List<GroupData> groups, string newGroupName)
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

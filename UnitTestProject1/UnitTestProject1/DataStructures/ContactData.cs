using System;

namespace addressbooktests
{
    public class ContactData : IEquatable<ContactData>, IComparable<ContactData>
    {
        private string firstName;
        private string middleName;
        private string lastName;
        private string nickName;
        private string title;
        private string company;
        private string address;
        private string home;
        private string mobile;
        private string email;
        private string[] bday;

        public ContactData(string nickName)
        {
            this.nickName = nickName;

            firstName = string.Empty;
            middleName = string.Empty;
            lastName = string.Empty;
            title = string.Empty;
            company = string.Empty;
            address = string.Empty;
            home = string.Empty;
            mobile = string.Empty;
            email = string.Empty;
        }

        public ContactData(string firstName, string lastName)
        {
            this.firstName = firstName;
            this.lastName = lastName;
        }

        public ContactData(string firstName, string middleName, string lastName)
        {
            this.firstName = firstName;
            this.middleName = middleName;
            this.lastName = lastName;
        }

        public string FirstName { get => firstName; set => firstName = value; }
        public string MiddleName { get => middleName; set => middleName = value; }
        public string LastName { get => lastName; set => lastName = value; }
        public string NickName { get => nickName; set => nickName = value; }
        public string Title { get => title; set => title = value; }
        public string Company { get => company; set => company = value; }
        public string Address { get => address; set => address = value; }
        public string Home { get => home; set => home = value; }
        public string Mobile { get => mobile; set => mobile = value; }
        public string Email { get => email; set => email = value; }
        public string[] Bday { get => bday; set => bday = value; }

        public bool Equals(ContactData other)
        {
            if (other is null)
            {
                return false;
            }
            if (Object.ReferenceEquals(this, other))
            {
                return true;
            }
            return firstName == other.firstName
                   && lastName == other.lastName;
        }

        public int CompareTo(ContactData other)
        {
            if (other is null)
            {
                return 1;
            }
            return LastName.CompareTo(other.LastName);
        }

        public override int GetHashCode()
        {
            return LastName.GetHashCode() + FirstName.GetHashCode();
        }

        public override string ToString()
        {
            return "Last Name = " + LastName + "\nFirst Name = " + FirstName;
        }
    }
}
using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace addressbooktests
{
    public class ContactData : IEquatable<ContactData>, IComparable<ContactData>
    {
        private string phones;
        private string emails;
        public ContactData()
        {

        }
        public ContactData(string nickName)
        {
            NickName = nickName;

            FirstName = string.Empty;
            MiddleName = string.Empty;
            LastName = string.Empty;
            Title = string.Empty;
            Company = string.Empty;
            Address = string.Empty;
            Home = string.Empty;
            Mobile = string.Empty;
            Email = string.Empty;
        }

        public ContactData(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        public ContactData(string firstName, string middleName, string lastName)
        {
            FirstName = firstName;
            MiddleName = middleName;
            LastName = lastName;
        }

        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string NickName { get; set; }
        public string Title { get; set; }
        public string Company { get; set; }
        public string Address { get; set; }
        public string Home { get; set; }
        public string Mobile { get; set; }
        public string Work { get; set; }
        public string Phones
        {
            get
            {
                if (phones != null)
                {
                    return phones;
                }
                else
                {
                    return (CleanUp(Home) + CleanUp(Mobile) + CleanUp(Work)).Trim();
                }
            }
            set => phones = value;
        }

        public string Email { get; set; }
        public string Email2 { get; set; }
        public string Email3 { get; set; }
        public string Emails
        {
            get
            {
                if (emails != null)
                {
                    return emails;
                }
                else
                {
                    return (CleanUp(Email) + CleanUp(Email2) + CleanUp(Email3)).Trim();
                }
            }
            set => emails = value;
        }
        public string[] Bday { get; set; }
        private int? Age
        {
            get
            {
                try
                {
                    var bMonth = DateTime.ParseExact(Bday[1], "MMMM", new CultureInfo("en-EN", false)).Month;
                    var today = DateTime.Today;
                    var age = today.Year - int.Parse(Bday[2]);
                    if (bMonth > today.Month)
                    {
                        age--;
                    }
                    else if (bMonth == today.Month)
                    {
                        if (int.Parse(Bday[0]) > today.Day)
                        {
                            age--;
                        }
                    }
                    return age;
                }
                catch(FormatException)
                {
                    return null;
                }
            }
        }

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
            return FirstName == other.FirstName
                   && LastName == other.LastName;
        }

        public int CompareTo(ContactData other)
        {
            if (other is null)
            {
                return 1;
            }
            int result = LastName.CompareTo(other.LastName);
            if (result == 0)
            {
                result = FirstName.CompareTo(other.FirstName);
            }
            return result;
        }

        public override int GetHashCode()
        {
            return LastName.GetHashCode() + FirstName.GetHashCode();
        }

        public override string ToString()
        {
            return "Last Name = " + LastName + "\nFirst Name = " + FirstName;
        }

        private string CleanUp(string phone)
        {
            if (phone == "" || phone is null)
            {
                return null;
            }
            return Regex.Replace(phone, "[ -()]", "") + "\r\n";
        }
        internal string ToGeneralInformation()
        {
            string fullName = $"{FirstName} {MiddleName} {LastName}";
            fullName.Trim();
            fullName.Replace("  ", " ");
            string bday = Bday is null ? null : $"Birthday {Bday[0]}. {Bday[1]} {Bday[2]} ({Age})";
            return $"{FirstName} {MiddleName} {LastName}{NickName}{Title}{Company}{Address}{Home}{Mobile}{Work}{Email}{Email2}{Email3}{bday}";
        }
    }
}
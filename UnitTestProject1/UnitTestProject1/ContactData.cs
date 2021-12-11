namespace addressbooktests
{
    public class ContactData
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
    }
}
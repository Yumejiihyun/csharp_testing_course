using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace addressbooktests
{
    class AccountData
    {
        private string username;
        private string password;

        public string Username { get => username; set => username = value; }
        public string Password { get => password; set => password = value; }
        public AccountData(string username, string password)
        {
            this.Username = username;
            this.Password = password;
        }
    }
}

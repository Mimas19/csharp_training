using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mantis_tests
{
    public class AccountData
    {
        public AccountData(String name, String password, String email)
        {
            Name = name;
            Password = password;
            Email = email;
        }
        
        public AccountData() {}  // добавила
        
        public string Name { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }
}
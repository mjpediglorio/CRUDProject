using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Db.Request.User
{
    public class DbRequestLoginByEmail
    {
        public DbRequestLoginByEmail(string Email, string Password)
        {
            this.Email = Email;
            this.Password = Password;
        }
        public DbRequestLoginByEmail()
        {
        }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}

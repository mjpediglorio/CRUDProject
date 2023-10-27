using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Db.Enums.Auth
{
    public enum OtpType
    {
        SignUp = 1,
        SignIn = 2,
        PasswordForget = 3,
        EmailActivation = 4
    }
}

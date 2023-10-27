using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Mailing.Enums
{
    public enum MailType
    {
        Account_Settings = 0,
        Account_2FA = 1,
        Blog_Survey = 2,
        Blog_Notification = 3,
        NewsLetter = 4,
        NoReply = 5
    }
}

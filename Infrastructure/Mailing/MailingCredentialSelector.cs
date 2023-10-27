using Infrastructure.Mailing.Enums;
using Infrastructure.Mailing.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Mailing
{
    public static class MailingCredentialSelector
    {
        public static MailingCredentialInfo GetCredentials(MailType mailType)
        {
            MailingCredentialInfo ret = new MailingCredentialInfo()
            {
                Email = "mykixxx24@gmail.com",
                Username = "AKIARS4H4FMVZK7SJGRS",
                Password = "BAUWfp34kLT5tGANSZT7R3ld8AZqGiiB7FJFc0H0ZJl+"
            };
            switch (mailType)
            {
                case MailType.Account_Settings:
                    ret.MailName = "accountsettings";
                    break;
                case MailType.Account_2FA:
                    ret.MailName = "2fa";
                    break;
                case MailType.Blog_Survey:
                    ret.MailName = "survey";
                    break;
                case MailType.Blog_Notification:
                    ret.MailName = "notifications";
                    break;
                case MailType.NewsLetter:
                    ret.MailName = "newsletter";
                    break;
                case MailType.NoReply:
                    ret.MailName = "noreply";
                    break;
                default:
                    ret.MailName = "hello";
                    break;
            }
            return ret;
        }
    }
}

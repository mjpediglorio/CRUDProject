using Domain.Db.Entities;
using Domain.Db.Enums.Auth;
using Domain.Db.Request.Mailing;
using Domain.Interfaces.Mailing;
using Domain.Mailing;
using Infrastructure.Mailing.Enums;
using Infrastructure.Mailing.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Mailing
{
    public class MailingService : IMail
    {

        public bool RegistrationActivationCodeSend(RequestMailRegistrationActivationCode mail, DbAuthUsers user)
        {
            MailingCredentialInfo credentials = MailingCredentialSelector.GetCredentials(MailType.NoReply);
            MailMessage email = CreateMailMessage(user, credentials, OtpType.SignUp, mail.RedirectUrl);
            return EmailSend(email, credentials);
        }

        public MailMessage CreateMailMessage(DbAuthUsers user, MailingCredentialInfo credentials, OtpType type, string content)
        {
            
            MailMessage email = new MailMessage();
            IMailContent emailContent = GenerateEmailContent(type);
            string body = emailContent.GenerateHtml(user, content);
            email.From = new MailAddress(credentials.Email);
            email.To.Add(user.Email);
            email.Subject = emailContent.EmailSubjectGet();
            email.Body = body;
            email.IsBodyHtml = true;
            return email;
        }

        public IMailContent GenerateEmailContent(OtpType type)
        {
            switch (type)
            {
                case OtpType.SignUp:
                    return new SignUp();
                default:
                    return new SignUp();
            }
        }
        public bool EmailSend(MailMessage email, MailingCredentialInfo credentials)
        {
            var basicCredentials = new NetworkCredential(credentials.Username, credentials.Password);
            SmtpClient smtpClient = new SmtpClient("email-smtp.ap-southeast-2.amazonaws.com", 587);
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = basicCredentials;
            smtpClient.EnableSsl = true;
            try
            {
                smtpClient.Send(email);
                Console.WriteLine("Email Sent Successfully");
                return true;
            }
            catch (Exception err)
            {
                Console.WriteLine($"Failed to send email{err.Message}");
                return false;
            }
        }
    }
}

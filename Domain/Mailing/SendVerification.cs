using Domain.Db.Entities;
using Domain.Interfaces.Mailing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Mailing
{
    public class SendVerification : IMailContent
    {
        public string EmailSubjectGet()
        {
            //TODO - Add Email Subject for SendVerification
            throw new NotImplementedException();
        }

        public string GenerateHtml(DbAuthUsers user, string content)
        {
            //TODO - Add HTML for SendVerification
            throw new NotImplementedException();
        }
    }
}

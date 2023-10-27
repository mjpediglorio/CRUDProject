using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Db.Request.Mailing
{
    public class RequestMailRegistrationActivationCode : RequestMailBase
    {
        public int Otp { get; set; }
        public string Hash { get; set; }
        public string RedirectUrl { get; set; }
    }
}

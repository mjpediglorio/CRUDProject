using Domain.Db.Enums.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Db.Request.User
{
    public record DbRequestGenerateOtp
    {
        public int UserId { get; set; }
        public OtpType Type { get; set; }
        public string Hash { get; set; }
        public int Code { get; set; }
        public string Email { get; set; }


    }
}

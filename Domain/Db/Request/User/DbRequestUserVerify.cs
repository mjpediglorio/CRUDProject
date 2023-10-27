using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Db.Request.User
{
    public record DbRequestUserVerify
    {
        public string Email { get; set; }
        public string Hash { get; set; }
        public int Code { get; set; }
    }
}

using Domain.Db.Result.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Db.Result.Auth
{
    public record DbResultGeneratedOtp : DbResult
    {
        public int Code { get; set; }
        public string Hash { get; set; }
    }
}
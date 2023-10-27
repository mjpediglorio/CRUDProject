using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Db.Enums.Auth
{
    public enum AuthStatusType
    {
        Inactive = 0,
        Active = 1,
        Blocked = 2,
        Fraud = 3
    }
}

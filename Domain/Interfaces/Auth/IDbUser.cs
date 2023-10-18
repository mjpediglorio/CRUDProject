using Domain.Db.Enums.Common;
using Domain.Db.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Auth
{
    public interface IDbUser
    {
        Task<DbResultTypes> Add(DbRequestUserAdd user);
    }
}

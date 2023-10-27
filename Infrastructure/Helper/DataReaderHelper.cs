using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Db.Entities;
using Domain.Db.Enums.Auth;

namespace Infrastructure.Helper
{
    public static class DataReaderHelper
    {
        public static DbAuthUsers ReaderToAuthUsers(this IDataReader reader)
        {
            return new DbAuthUsers()
            {
                Id = reader["Id"].ObjectToInt(),
                FirstName = reader["FirstName"].ToString(),
                MiddleName = reader["MiddleName"].ToString(),
                LastName = reader["LastName"].ToString(),
                Email = reader["Email"].ToString(),
                ContactNumber = reader["ContactNumber"].ToString(),
                ProfilePicture = reader["ProfilePicture"].ToString(),
                IsVerified = reader["IsVerified"].ObjectToInt(),
                UserRoles = (UserRolesType)reader["UserRoles"].ObjectToInt(),
                Status = (AuthStatusType)reader["Status"].ObjectToInt()
            };
        }
    }
}

using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Helper
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
                Email = reader["Email"].ToString()
            };
        }
    }
}

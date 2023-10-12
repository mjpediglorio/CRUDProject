using DataAccess.Models;
using DataAccess.Helper;

namespace DataAccess.DbAccess
{
    public interface ISqlDataAccess
    {
        Task<DbResultTypes> LoginByEmail(UserLoginModel request);
        Task<DbAuthUsers> UserDetailsGetByEmail(string email);
        Task<DbResultTypes> AddUser(UserRegisterModel user);
    }
}
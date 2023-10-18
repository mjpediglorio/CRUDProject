using DataAccess.Models;
using DataAccess.Helper;

namespace Application.DbAccess.DbUser
{
    public interface ISqlDataAccess
    {
        Task<DbResultTypes> LoginByEmail(UserLoginModel request);
        Task<DbAuthUsers> UserDetailsGetByEmail(string email);
        Task<DbResultTypes> AddUser(UserRegisterModel user);

        Task<DbAuthUsers> GetInfo(int userId);
    }
}
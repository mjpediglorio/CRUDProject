using DataAccess.Models;
using DataAccess.Helper;
using Application.UserRegister;

namespace Application.DbAccess.DbUser
{
    public interface IDbUser
    {
        Task<DbResultTypes> LoginByEmail(UserLoginModel request);
        Task<DbAuthUsers> UserDetailsGetByEmail(string email);
        Task<DbResultTypes> AddUser(UserRegisterModel user);

        Task<DbAuthUsers> GetInfo(int userId);
    }
}
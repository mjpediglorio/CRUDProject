using DataAccess.DbAccess;
using DataAccess.Models;

namespace DataAccess.Data
{
    public interface IUserData
    {
        Task<SignUpDto> RegisterUser(UserRegisterModel user);
        Task<AuthDto> UserLogin(UserLoginModel user);
    }
}
using Domain.Db.Entities;
using Domain.Db.Enums.Common;
using Domain.Db.Request.User;
using Domain.Db.Result.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Auth
{
    public interface IDbUser
    {
        Task<DbResultTypes> UserAdd(DbRequestUserAdd user);
        Task<DbResultTypes> LoginByEmail(DbRequestLoginByEmail user);
        Task<DbAuthUsers> GetInfoByEmail(string email);
        Task<DbAuthUsers> GetInfoById(int id);
        Task<DbResultTypes> UserDelete(DbRequestUserDelete user, int userId);
        Task<DbResultTypes> UserInfoUpdate(DbRequestUserInfoUpdate user, int userId);
        Task<DbResultGeneratedOtp> GenerateOtp(DbRequestGenerateOtp otp);

        Task<DbResultTypes> VerifyEmail(DbRequestUserVerify request);
    }
}

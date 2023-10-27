using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Interfaces.Auth;
using Domain.Db.Enums.Common;
using Domain.Db.Enums.Auth;

namespace Application.User.UserInfoGet
{
    public class UserInfoGetCommand : IUserInfoGetCommand
    {
        private readonly IDbUser _db;
        public UserInfoGetCommand(IDbUser db)
        {
            _db = db;
        }
        public async Task<UserInfoGetDto> UserInfoGet(int userId)
        {
            UserInfoGetDto ret = new UserInfoGetDto();
            var res = await _db.GetInfoById(userId);
            if (res.Result == DbResultTypes.OK)
            {
                ret.Id = res.Id;
                ret.FirstName = res.FirstName;
                ret.MiddleName = res.MiddleName;
                ret.LastName = res.LastName;
                ret.Email = res.Email;
                ret.ContactNumber = res.ContactNumber;
                ret.ProfilePicture = res.ProfilePicture;
                ret.IsVerified = res.IsVerified;
                ret.UserRoles = res.UserRoles;
                ret.Status = res.Status;
            }
            else
            {
                ret.Code = (int)res.Result;
                ret.Message = res.ToString();
                ret.ErrorMsg = res.ToString();
            }
            return ret;
        }
    }
}

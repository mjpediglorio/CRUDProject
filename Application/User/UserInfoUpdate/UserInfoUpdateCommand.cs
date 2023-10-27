using Application.Common.Models;
using Domain.Db.Request;
using Domain.Interfaces.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Db.Enums.Common;
using Application.UserRegister;
using Domain.Db.Request.User;

namespace Application.User.UserInfoUpdate
{
    public class UserInfoUpdateCommand : IUserInfoUpdateCommand
    {
        private readonly IDbUser _db;
        public UserInfoUpdateCommand(IDbUser db)
        {
            _db = db;
        }

        public async Task<UserInfoUpdateDto> InfoUpdate(int userId, UserInfoUpdateModel user)
        {
            var ret = new UserInfoUpdateDto();
            var valid = await Validate(user);
            if (valid.IsValid == false)
            {
                ret.IsSuccessful = false;
                ret.ErrorMsg = valid.ErrorMsg;
            }
            else
            {
                var request = new DbRequestUserInfoUpdate
                {
                    Username = user.Username,
                    FirstName = user.FirstName,
                    LastName = user.MiddleName,
                    MiddleName = user.LastName,
                    ContactNumber = user.ContactNumber
                };
                var res = await _db.UserInfoUpdate(request, userId);
                if (res == DbResultTypes.OK)
                {
                    ret.Message = "You have done a good job!";
                    ret.IsSuccessful = true;
                }
                else
                {
                    ret.Code = (int)res;
                    ret.Message = res.ToString();
                    ret.Message = "You have done a good job!";
                    ret.IsSuccessful = false;
                }

            }
            return ret;
        }

        private async Task<ValidateInfoDto> Validate(UserInfoUpdateModel user)
        {
            var ret = new ValidateInfoDto();
            ret.IsValid = true;
            if (user.FirstName == "" && user.Username == "" && user.MiddleName == "" && user.LastName == "" && user.ContactNumber == "")
            {
                ret.IsValid = false;
                ret.ErrorMsg = "ALL INFO IS NULL";
            }
            if (user.FirstName == null && user.Username == null && user.MiddleName == null && user.LastName == null && user.ContactNumber == null)
            {
                ret.IsValid = false;
                ret.ErrorMsg = "ALL INFO IS NULL";
            }
            if (user.FirstName == "string" || user.Username == "string" || user.MiddleName == "string" || user.LastName == "string" || user.ContactNumber == "string")
            {
                ret.IsValid = false;
                ret.ErrorMsg = "INFO CONNTAINS 'STRING'";
            }
            return ret;
        }
        public class ValidateInfoDto : ResultBaseDto
        {
            public Boolean IsValid { get; set; }
        }
    }
}

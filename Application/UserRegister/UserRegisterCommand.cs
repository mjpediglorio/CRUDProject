using Application.Common.Models;
using Domain.Db.Enums.Auth;
using Domain.Db.Enums.Common;
using Domain.Db.Request;
using Domain.Db.Request.Mailing;
using Domain.Db.Request.User;
using Domain.Interfaces.Auth;
using Domain.Interfaces.Mailing;
using Infrastructure.Mailing;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using static Application.User.UserInfoUpdate.UserInfoUpdateCommand;

namespace Application.UserRegister
{
    public class UserRegisterCommand : IUserRegisterCommand
    {
        private readonly IDbUser _db;
        public UserRegisterCommand(IDbUser db)
        {
            _db = db;
        }
        public async Task<UserRegisterDto> Register(UserRegisterModel request, string confirmationLink, int code, string hash)
        {
            var IsValid = await Validate(request);
            if (IsValid.IsValid ==false)
            {
                return new UserRegisterDto()
                {
                    ErrorMsg = IsValid.ErrorMsg,
                    IsSuccessful = false
                };
            }
            MailingService mailService = new MailingService();
            var user = new DbRequestUserAdd
            {
                Username = request.Username,
                FirstName = request.FirstName,
                MiddleName = request.MiddleName,
                LastName = request.LastName,
                Email = request.Email,
                Password = request.Password,
                ContactNumber = request.ContactNumber
            };
            var res = await _db.UserAdd(user);
            if (res == DbResultTypes.OK)
            {
                var userDetails = await _db.GetInfoByEmail(request.Email);
                var resultOTP = await _db.GenerateOtp(new DbRequestGenerateOtp()
                {
                    Hash = hash,
                    Code = code,
                    Email = request.Email,
                    UserId = userDetails.Id,
                    Type = OtpType.SignUp
                });
                if (resultOTP.Result == DbResultTypes.OK)
                {
                    var req = new RequestMailRegistrationActivationCode()
                    {
                        Hash = resultOTP.Hash,
                        RedirectUrl = confirmationLink,
                        Otp = resultOTP.Code
                    };
                    mailService.RegistrationActivationCodeSend(req, userDetails);
                    return new UserRegisterDto()
                    {
                        IsSuccessful = true
                    };
                }
                else
                {
                    return new UserRegisterDto()
                    {
                        IsSuccessful = false,
                        Code = (int)res,
                        Message = res.ToString()
                    };
                }

            }
            else return new UserRegisterDto()
            {
                Code = (int)res,
                Message = res.ToString(),
                ErrorMsg = "",
                IsSuccessful = false
            };
        }

        public async Task<ValidatorDto> Validate(UserRegisterModel request)
        {
            var ret = new ValidatorDto();
            ret.IsValid = true;
            if (request.Email == ""
                && request.Username == ""
                && request.FirstName == ""
                && request.Username == ""
                && request.MiddleName == ""
                && request.LastName == ""
                && request.ContactNumber == "")
            {
                ret.IsValid = false;
                ret.ErrorMsg = "ALL INFO IS NULL";
            }
            if (request.Email == null
                && request.Username == null
                && request.FirstName == null
                && request.Username == null
                && request.MiddleName == null
                && request.LastName == null
                && request.ContactNumber == null)
            {
                ret.IsValid = false;
                ret.ErrorMsg = "ALL INFO IS NULL";
            }
            if (request.Email == "string"
                || request.Username == "string"
                || request.FirstName == "string"
                || request.Username == "string"
                || request.MiddleName == "string"
                || request.LastName == "string"
                || request.ContactNumber == "string")
            {
                ret.IsValid = false;
                ret.ErrorMsg = "INFO CONNTAINS 'STRING'";
            }
            return ret;
        }
        public class ValidatorDto : ResultBaseDto
        {
            public bool IsValid { get; set; }
        }
    }
}

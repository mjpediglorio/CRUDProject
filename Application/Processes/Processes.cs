using Application.DbAccess.DbUser;
using Application.Models;
using Application.UserRegister;
using DataAccess.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.Processes
{
    public class Processes
    {
        private IConfiguration _config;
        private readonly IDbUser _db;

        public Processes(IDbUser db, IConfiguration config)
        {
            _db = db;
            _config = config;
        }

        public async Task<UserRegisterDto> Register(UserRegisterModel user)
        {
            var res = await _db.AddUser(user);
            if (res == DbResultTypes.OK)
            {
                return new UserRegisterDto()
                {
                    IsSuccessful = true
                };
            }
            else return new UserRegisterDto()
            {
                IsSuccessful = false
            };
        }

        //public async Task<AuthDto> Login(UserLoginModel request)
        //{
        //    var res = await _db.LoginByEmail(request);
        //    if (res == DbResultTypes.OK)
        //    {
        //        var tokenString = "";
        //        var account_details = await _db.UserDetailsGetByEmail(request.Email);
        //        var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));
        //        var creds = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256Signature);
        //        var tokenOptions = new JwtSecurityToken(
        //            expires: DateTime.Now.AddDays(7),
        //            signingCredentials: creds,
        //            claims: new List<Claim>()
        //            {
        //                new Claim(ClaimTypes.Role, "user"),
        //                new Claim(ClaimTypes.Name, account_details.Id.ToString())
        //            }
        //        );
        //        tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        //        return new AuthDto
        //        {
        //            Id = account_details.Id,
        //            Code = (int)res,
        //            Message = res.ToString(),
        //            ErrorMsg = res.ToString(),
        //            Token = tokenString
        //        };
        //    }
        //    else
        //    {
        //        return new AuthDto
        //        {
        //            Code = (int)res,
        //            Message = res.ToString(),
        //            ErrorMsg = res.ToString(),
        //            Token = null
        //        };
        //    }
        //}

        //public async Task<AccountInfoDto> GetUserInfo(int userId)
        //{
        //    AccountInfoDto ret = new AccountInfoDto();
        //    var res = await _db.GetInfo(userId);
        //    if (res.Result == DbResultTypes.OK)
        //    {
        //        ret.FirstName = res.FirstName;
        //        ret.MiddleName = res.MiddleName;
        //        ret.LastName = res.LastName;
        //        ret.Email = res.Email;
        //        return ret;
        //    }
        //    else
        //    {
        //        return new AccountInfoDto
        //        {
        //            Code = (int)res.Result,
        //            Message = res.ToString(),
        //            ErrorMsg = res.ToString(),

        //        };
        //    }
        //}
    }
}
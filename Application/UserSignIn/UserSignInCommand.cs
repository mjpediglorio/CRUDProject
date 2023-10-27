using Domain.Db.Enums.Common;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Interfaces.Auth;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;
using Domain.Db.Request.User;

namespace Application.UserSignIn
{
    public class UserSignInCommand : IUserSignInCommand
    {
        private readonly IDbUser _db;
        private readonly IConfiguration _config;
        public UserSignInCommand(IDbUser db, IConfiguration configuration)
        {
            _config = configuration;
            _db = db;
        }
        public async Task<SignInDto> SignInCommand(UserSignInModel user)
        {
            DbRequestLoginByEmail request = new DbRequestLoginByEmail(user.Email, user.Password);
            var res = await _db.LoginByEmail(request);
            if (res == DbResultTypes.OK)
            {
                var tokenstring = "";
                var account_details = await _db.GetInfoByEmail(user.Email);
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));
                var creds = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256Signature);
                var tokenoptions = new JwtSecurityToken(
                    expires: DateTime.Now.AddDays(7),
                    signingCredentials: creds,
                    claims: new List<Claim>()
                    {
                            new Claim(ClaimTypes.Role, "user"),
                            new Claim(ClaimTypes.Name, account_details.Id.ToString())
                    }
                );
                tokenstring = new JwtSecurityTokenHandler().WriteToken(tokenoptions);
                return new SignInDto
                {
                    Id = account_details.Id,
                    Code = (int)res,
                    Message = res.ToString(),
                    ErrorMsg = res.ToString(),
                    Token = tokenstring
                };
            }
            else
            {
                return new SignInDto
                {
                    Code = (int)res,
                    Message = res.ToString(),
                    ErrorMsg = res.ToString(),
                    Token = null
                };
            }
        }
    }
}


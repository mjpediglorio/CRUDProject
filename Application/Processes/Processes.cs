using DataAccess.DbAccess;
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
    public class Processes : IProcesses
    {
        private IConfiguration _config;
        private readonly ISqlDataAccess _db;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public Processes(ISqlDataAccess db, IConfiguration config)
        {
            _db = db;
            _config = config;
        }

        public async Task<SignUpDto> Register(UserRegisterModel user)
        {
            var res = await _db.AddUser(user);
            if (res == DbResultTypes.OK)
            {
                return new SignUpDto()
                {
                    IsSuccessful = true
                };
            }
            else return new SignUpDto()
            {
                IsSuccessful = false
            };
        }

        public async Task<IAsyncResult> InfoGet()
        {
            var res = int.Parse(_httpContextAccessor.HttpContext.User.Identity.Name);
        }
        public async Task<AuthDto> Login(UserLoginModel request)
        {
            var res = await _db.LoginByEmail(request);
            if (res == DbResultTypes.OK)
            {
                var tokenString = "";
                var account_details = await _db.UserDetailsGetByEmail(request.Email);
                Console.WriteLine(account_details);
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));
                var creds = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256Signature);
                var tokenOptions = new JwtSecurityToken(
                    expires: DateTime.Now.AddDays(7),
                    signingCredentials: creds,
                    claims: new List<Claim>()
                    {
                        new Claim(ClaimTypes.Role, "user"),
                        new Claim(ClaimTypes.Name, account_details.Id.ToString())
                    }
                );
                tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
                return new AuthDto
                {
                    Id = account_details.Id,
                    Code = (int)res,
                    Message = res.ToString(),
                    ErrorMsg = res.ToString(),
                    Token = tokenString
                };
            }
            else
            {
                return new AuthDto
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
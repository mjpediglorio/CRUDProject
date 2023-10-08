using Dapper;
using DataAccess.DbAccess;
using DataAccess.Models;
using DataAccess.Services;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Data
{
    public class UserData : IUserData
    {
        private IConfiguration _config;
        private readonly ISqlDataAccess _db;
        private readonly IUserService _userService;
        public UserData(ISqlDataAccess db, IConfiguration config)
        {
            _db = db;
            _config = config;
        }
        public async Task<SignUpDto> RegisterUser(UserRegisterModel user)
        {
            SignUpDto result = new SignUpDto();
            using (IDbConnection connection = new SqlConnection(_config.GetConnectionString("Default")))
            {
                using (var cmd = connection.CreateCommand())
                {
                    cmd.Connection = connection;
                    connection.Open();
                    cmd.CommandText = "INSERT INTO dbo.UserDB (FirstName, MiddleName, LastName, Email, Password)" +
                    "VALUES (@FirstName, @MiddleName, @LastName, @Email, @Password);";
                    cmd.Parameters.Add(new SqlParameter("@FirstName", user.FirstName));
                    cmd.Parameters.Add(new SqlParameter("@MiddleName", user.MiddleName));
                    cmd.Parameters.Add(new SqlParameter("@LastName", user.LastName));
                    cmd.Parameters.Add(new SqlParameter("@Email", user.Email));
                    cmd.Parameters.Add(new SqlParameter("@Password", user.Password));
                    int a = cmd.ExecuteNonQuery();
                    if (a == 0)
                    {
                        result.IsSuccessful = false;
                    }
                    else
                    {
                        result.IsSuccessful = true;
                    }
                }
            };
            return result;
        }

        public async Task<AuthDto> UserLogin(UserLoginModel request)
        {
            var res = await _db.LoginByEmail(request);
            if (res == DbResultTypes.OK)
            {
                var tokenString = "";
                var account_details = await _db.UserDetailsGetByEmail(request.Email);
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

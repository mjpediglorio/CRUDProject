﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Runtime.CompilerServices;
using Microsoft.Extensions.Configuration;
using DataAccess.Models;
using Infrastructre.Helper;

namespace Application.DbAccess.DbUser
{
    public class DbUser : ISqlDataAccess
    {
        private IConfiguration _config;

        public DbUser(IConfiguration config)
        {
            _config = config;
        }

        public async Task<DbResultTypes> AddUser(UserRegisterModel user)
        {
            using (IDbConnection connection = new SqlConnection(_config.GetConnectionString("Default")))
            {
                using (var cmd = connection.CreateCommand())
                {
                    cmd.Connection = connection;
                    connection.Open();
                    cmd.CommandText = "INSERT INTO dbo.UsersDB (Username, FirstName, MiddleName, LastName, ContactNumber, Email, Password)" +
                    "VALUES (@Username, @FirstName, @MiddleName, @LastName, @ContactNumber, @Email, @Password);";
                    cmd.Parameters.Add(new SqlParameter("@Username", user.Username));
                    cmd.Parameters.Add(new SqlParameter("@FirstName", user.FirstName));
                    cmd.Parameters.Add(new SqlParameter("@MiddleName", user.MiddleName));
                    cmd.Parameters.Add(new SqlParameter("@LastName", user.LastName));
                    cmd.Parameters.Add(new SqlParameter("@ContactNumber", user.ContactNumber));
                    cmd.Parameters.Add(new SqlParameter("@Email", user.Email));
                    cmd.Parameters.Add(new SqlParameter("@Password", user.Password));
                    int a = cmd.ExecuteNonQuery();
                    if (a == 0)
                    {
                        return DbResultTypes.INVALID_REQUEST;
                    }
                    else
                    {
                        return DbResultTypes.OK;
                    }
                }
            };
        }

        public Task<DbAuthUsers> GetInfo(int userId)
        {
            throw new NotImplementedException();
        }

        public async Task<DbResultTypes> LoginByEmail(UserLoginModel request)
        {
            if (request.Email == "" || request.Password == "")
            {
                return DbResultTypes.INVALID_REQUEST;
            }
            try
            {
                using (IDbConnection connection = new SqlConnection(_config.GetConnectionString("Default")))
                {
                    using (var cmd = connection.CreateCommand())
                    {
                        cmd.Connection = connection;
                        connection.Open();
                        cmd.CommandText = " SELECT COUNT(Id) FROM UserDB WHERE Email = @Email AND Password = @Password";
                        cmd.Parameters.Add(new SqlParameter("@Email", request.Email));
                        cmd.Parameters.Add(new SqlParameter("@Password", request.Password));
                        var output = cmd.ExecuteScalar().ToString();
                        int userId = int.Parse(output);
                        if (userId != 0)
                        {
                            return DbResultTypes.OK;
                        }
                        return DbResultTypes.INVALID_CREDENTIAL;
                    }
                }
            }
            catch (Exception err)
            {
                throw new CustomException(err.ToString())
                {
                    Code = (int)DbResultTypes.DB_EXCEPTION,
                    Message = err.ToString() + "test"
                };
                throw;
            }

        }

        public async Task<DbAuthUsers> UserDetailsGetByEmail(string email)
        {
            if (string.IsNullOrEmpty(email)) return new DbAuthUsers()
            {
                Result = DbResultTypes.INVALID_REQUEST
            };

            DbAuthUsers dbauthuser = new DbAuthUsers();

            try
            {
                using (IDbConnection connection = new SqlConnection(_config.GetConnectionString("Default")))
                {
                    using (var cmd = connection.CreateCommand())
                    {
                        cmd.CommandTimeout = 120;
                        cmd.Connection = connection;
                        connection.Open();
                        cmd.CommandText = "SELECT TOP 1 * from UserDB WHERE Email = @Email";
                        cmd.Parameters.Add(new SqlParameter("@Email", email));

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                dbauthuser = reader.ReaderToAuthUsers();
                            }
                        }
                    }
                    dbauthuser.Result = DbResultTypes.OK;
                }
            }
            catch (Exception err)
            {
                throw new CustomException(err.ToString())
                {
                    Code = (int)DbResultTypes.DB_EXCEPTION,
                    Message = err.ToString()
                };
            }
            return dbauthuser;
        }
    }
}

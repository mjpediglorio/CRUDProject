using System;
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
using Domain.Db.Enums;
using System.Data.SqlClient;
using Domain.Db.Enums.Common;
using Domain.Exceptions;
using Domain.Db.Entities;
using Azure.Core;
using Domain.Db.Request.User;
using Domain.Db.Result.Auth;
using Infrastructure.Utilities;
using Domain.Interfaces.Auth;
using Infrastructure.Helper;

namespace Infrastructure.DbApp
{
    public class DbUser : IDbUser
    {
        private IConfiguration _config;

        public DbUser(IConfiguration config)
        {
            _config = config;
        }

        public async Task<DbResultTypes> AccountValidate(int userId)
        {
            if (userId < 0 || userId == null)
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
                        cmd.CommandText = "SELECT COUNT(Email) From dbo.UsersDB WHERE Id = @Id";
                        cmd.Parameters.Add(new SqlParameter("@Id", userId));
                        var output = cmd.ExecuteScalar().ObjectToInt();
                        if (output == 0)
                        {
                            return DbResultTypes.NOT_FOUND;
                        }
                        else
                        {
                            return DbResultTypes.OK;
                        }
                    }
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
        }
        public async Task<DbResultTypes> EmailValidate(string? email)
        {
            if (string.IsNullOrEmpty(email))
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
                        cmd.CommandText = "SELECT COUNT(Email) From dbo.UsersDB WHERE Email = @Email";
                        cmd.Parameters.Add(new SqlParameter("@Email", email));
                        var output = cmd.ExecuteScalar().ObjectToInt();
                        if (output == 0)
                        {
                            return DbResultTypes.NOT_FOUND;
                        }
                        else
                        {
                            return DbResultTypes.OK;
                        }
                    }
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
        }

        public async Task<DbResultTypes> UsernameValidate(string? username)
        {
            if (string.IsNullOrEmpty(username))
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
                        cmd.CommandText = "SELECT COUNT(Username) From dbo.UsersDB WHERE Email = @Username";
                        cmd.Parameters.Add(new SqlParameter("@Username", username));
                        var output = cmd.ExecuteScalar().ObjectToInt();
                        if (output == 0)
                        {
                            return DbResultTypes.NOT_FOUND;
                        }
                        else
                        {
                            return DbResultTypes.OK;
                        }
                    }
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
        }
        public async Task<DbResultTypes> UserAdd(DbRequestUserAdd user)
        {
            if (await EmailValidate(user.Email) == DbResultTypes.OK)
            {
                return DbResultTypes.EXISTING_EMAIL;
            }
            if (await UsernameValidate(user.Username) == DbResultTypes.OK)
            {
                return DbResultTypes.EXISTING_USERNAME;
            }
            using (IDbConnection connection = new SqlConnection(_config.GetConnectionString("Default")))
            {
                using (var cmd = connection.CreateCommand())
                {
                    cmd.Connection.Open();
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
        public async Task<DbResultTypes> PasswordCheckById(int userId, string password)
        {
            if (userId == null || password == null)
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
                        cmd.CommandText = " SELECT COUNT(Id) FROM UsersDB WHERE Id = @Id AND Password = @Password";
                        cmd.Parameters.Add(new SqlParameter("@Id", userId));
                        cmd.Parameters.Add(new SqlParameter("@Password", password));
                        var output = int.Parse(cmd.ExecuteScalar().ToString());
                        if (output != 0)
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
            }
        }
        public async Task<DbResultTypes> LoginByEmail(DbRequestLoginByEmail user)
        {
            if (user.Email == "" || user.Password == "")
            {
                return DbResultTypes.INVALID_REQUEST;
            }
            try
            {
                using (IDbConnection connection = new SqlConnection(_config.GetConnectionString("Default")))
                {
                    using (var cmd = connection.CreateCommand())
                    {
                        cmd.Connection.Open();
                        cmd.CommandText = " SELECT COUNT(Id) FROM UsersDB WHERE Email = @Email AND Password = @Password";
                        cmd.Parameters.Add(new SqlParameter("@Email", user.Email));
                        cmd.Parameters.Add(new SqlParameter("@Password", user.Password));
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

        public async Task<DbAuthUsers> GetInfoByEmail(string email)
        {
            DbAuthUsers dbauthuser = new DbAuthUsers();
            if (string.IsNullOrEmpty(email))
            {
                dbauthuser.Result = DbResultTypes.INVALID_REQUEST;
                return dbauthuser;
            };
            try
            {
                using (IDbConnection connection = new SqlConnection(_config.GetConnectionString("Default")))
                {
                    using (var cmd = connection.CreateCommand())
                    {
                        cmd.CommandTimeout = 120;
                        cmd.Connection = connection;
                        connection.Open();
                        cmd.CommandText = "SELECT * from UsersDB WHERE Email = @Email";
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
        public async Task<DbResultGeneratedOtp> GenerateOtp(DbRequestGenerateOtp request)
        {
            DbResultGeneratedOtp ret = new DbResultGeneratedOtp();
            DbResultTypes res;
            Random rnd = new Random();
            var code = request.Code;
            var hash = request.Hash;
            DateTime createdDate = ServerTime.GetServerTime();
            try
            {
                using (IDbConnection connection = new SqlConnection(_config.GetConnectionString("Default")))
                {
                    using (var cmd = connection.CreateCommand())
                    {
                        cmd.CommandTimeout = 120;
                        cmd.Connection = connection;
                        connection.Open();
                        cmd.CommandText = "INSERT INTO dbo.OtpDB (UserId, Code, Hash, IsActive, OtpType, ExpirationDate, CreatedDate, ModifiedDate)" +
                            "VALUES(@UserId, @Code, @Hash, @IsActive, @OtpType, @ExpirationDate, @CreatedDate, @ModifiedDate)";
                        cmd.Parameters.Add(new SqlParameter("@UserId", request.UserId));
                        cmd.Parameters.Add(new SqlParameter("@Code", code));
                        cmd.Parameters.Add(new SqlParameter("@Hash", hash));
                        cmd.Parameters.Add(new SqlParameter("@IsActive", 1));
                        cmd.Parameters.Add(new SqlParameter("@OtpType", request.Type));
                        cmd.Parameters.Add(new SqlParameter("@ExpirationDate", createdDate.AddMinutes(5).DateTimeToUnixTime()));
                        cmd.Parameters.Add(new SqlParameter("@CreatedDate", createdDate.DateTimeToUnixTime()));
                        cmd.Parameters.Add(new SqlParameter("@ModifiedDate", createdDate.DateTimeToUnixTime()));
                        res = cmd.ExecuteNonQuery() > 0 ? DbResultTypes.OK : DbResultTypes.INVALID_REQUEST;
                    }
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

            if (res == DbResultTypes.OK)
            {
                ret.Code = code;
                ret.Hash = hash;
            }
            else
            {
                ret.Result = res;
            }
            return ret;
        }
        public async Task<DbResultTypes> UserDelete(DbRequestUserDelete user, int userId)
        {

            var infocheck = await PasswordCheckById(userId, user.Password);
            if (infocheck == DbResultTypes.OK)
            {
                using (IDbConnection connection = new SqlConnection(_config.GetConnectionString("Default")))
                {
                    using (var cmd = connection.CreateCommand())
                    {
                        cmd.CommandTimeout = 120;
                        cmd.Connection = connection;
                        connection.Open();
                        cmd.CommandText = "DELETE from UsersDB WHERE Id = @Id";
                        cmd.Parameters.Add(new SqlParameter("@Id", userId));
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
                }
            }
            else
            {
                return DbResultTypes.INVALID_PASSWORD;
            }
        }
        public async Task<DbAuthUsers> GetInfoById(int id)
        {
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
                        cmd.CommandText = "SELECT * from UsersDB WHERE Id = @Id";
                        cmd.Parameters.Add(new SqlParameter("@Id", id));
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


        public async Task<DbResultTypes> UserInfoUpdate(DbRequestUserInfoUpdate user, int userId)
        {
            DbResultTypes ret = new DbResultTypes();
            try
            {
                using (IDbConnection connection = new SqlConnection(_config.GetConnectionString("Default")))
                {
                    using (var cmd = connection.CreateCommand())
                    {
                        cmd.Connection = connection;
                        connection.Open();
                        var commandText = "UPDATE UsersDB SET ";
                        if (!string.IsNullOrEmpty(user.Username))
                        {
                            commandText += "Username = @Username, ";
                            cmd.Parameters.Add(new SqlParameter("@Username", user.Username));
                        }

                        if (!string.IsNullOrEmpty(user.FirstName))
                        {
                            commandText += "FirstName = @FirstName, ";
                            cmd.Parameters.Add(new SqlParameter("@FirstName", user.FirstName));
                        }

                        if (!string.IsNullOrEmpty(user.MiddleName))
                        {
                            commandText += "MiddleName = @MiddleName, ";
                            cmd.Parameters.Add(new SqlParameter("@MiddleName", user.MiddleName));
                        }

                        if (!string.IsNullOrEmpty(user.LastName))
                        {
                            commandText += "LastName = @LastName, ";
                            cmd.Parameters.Add(new SqlParameter("@LastName", user.LastName));
                        }

                        if (!string.IsNullOrEmpty(user.ContactNumber))
                        {
                            commandText += "ContactNumber = @ContactNumber, ";
                            cmd.Parameters.Add(new SqlParameter("@ContactNumber", user.ContactNumber));
                        }
                        cmd.Parameters.Add(new SqlParameter("@Id", userId));
                        commandText = commandText.TrimEnd(' ', ',') + " WHERE Id = @Id;";
                        cmd.CommandText = commandText;
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
            return ret;
        }

        public Task<DbResultTypes> VerifyEmail(DbRequestUserVerify request)
        {
            throw new NotImplementedException();
        }
    }
}
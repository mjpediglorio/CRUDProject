using Domain.Db.Enums.Common;
using Domain.Db.Request;
using Domain.Interfaces.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UserRegister
{
    public class UserRegisterCommand : IUserRegisterCommand
    {
        private readonly IDbUser _db;
        public UserRegisterCommand(IDbUser db)
        {
            _db = db;
        }
        public async Task<UserRegisterDto> Register(UserRegisterModel request)
        {
            var user = new DbRequestUserAdd
            {
                FirstName = request.FirstName,
                MiddleName  = request.MiddleName,
                LastName = request.LastName,
                Email = request.Email,
                Password = request.Password,
                ContactNumber = request.ContactNumber
            };
            var res = await _db.Add(user);
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
    }
}

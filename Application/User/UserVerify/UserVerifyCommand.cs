using Domain.Db.Request.User;
using Domain.Interfaces.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.User.UserVerify
{
    public class UserVerifyCommand : IUserVerifyCommand
    {
        private readonly IDbUser _db;
        public UserVerifyCommand(IDbUser db)
        {
            _db = db;
        }
        public async Task<UserVerifyDto> UserVerify(UserVerifyModel user)
        {

            DbRequestUserVerify request = new DbRequestUserVerify()
            {
                Email = user.Email,
                Code = user.Code,
                Hash = user.Hash
            };
            var result = await _db.VerifyEmail(request);
            return new UserVerifyDto();
        }
    }
}

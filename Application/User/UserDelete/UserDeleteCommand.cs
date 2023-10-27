using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Db.Enums.Common;
using Domain.Db.Request.User;
using Domain.Interfaces.Auth;

namespace Application.User.UserDelete
{
    public class UserDeleteCommand : IUserDelete
    {
        private IDbUser _db;
        public UserDeleteCommand(IDbUser db)
        {
            _db = db;
        }
        public async Task<UserDeleteDto> UserDelete(int userId, UserDeleteModel user)
        {
            UserDeleteDto ret = new UserDeleteDto();
            DbRequestUserDelete request = new DbRequestUserDelete();
            {
                request.Email = user.Email;
                request.Password = user.Password;
            }
            var res = await _db.UserDelete(request, userId);

            if (res == DbResultTypes.OK)
            {
                ret.IsSuccessful = true;
            }
            else
            {
                ret.Code = (int)res;
                ret.Message = res.ToString();
                ret.ErrorMsg = res.ToString();
            }
            return ret;
        }
    }
}
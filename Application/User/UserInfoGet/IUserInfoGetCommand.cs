using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.User.UserInfoGet
{
    public interface IUserInfoGetCommand
    {
        Task<UserInfoGetDto> UserInfoGet(int userId);
    }
}

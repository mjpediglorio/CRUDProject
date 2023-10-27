using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.User.UserInfoUpdate
{
    public interface IUserInfoUpdateCommand
    {
        Task<UserInfoUpdateDto> InfoUpdate(int userId, UserInfoUpdateModel user);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.User.UserDelete
{
    public interface IUserDelete
    {
        Task<UserDeleteDto> UserDelete(int userId, UserDeleteModel user);
    }
}

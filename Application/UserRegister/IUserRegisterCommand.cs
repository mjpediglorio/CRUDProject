using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UserRegister
{
    public interface IUserRegisterCommand
    {
        Task<UserRegisterDto> Register(UserRegisterModel request, string confirmationLink, int code, string hash);
    }
}

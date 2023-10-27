using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UserSignIn
{
    public interface IUserSignInCommand
    {
        Task<SignInDto> SignInCommand(UserSignInModel user);
    }
}

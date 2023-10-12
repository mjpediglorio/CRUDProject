using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Processes
{
    public interface IProcesses
    {
        Task<AuthDto> Login(UserLoginModel request);
        Task<SignUpDto> Register(UserRegisterModel user);
    }
}

using Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UserRegister
{
    public class UserRegisterDto : ResultBaseDto
    {
        public Boolean? IsSuccessful { get; set; }
    }
}

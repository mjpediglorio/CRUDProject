using Domain.Db.Entities;
using Domain.Db.Request.Mailing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Mailing
{
    public interface IMail
    {
        bool RegistrationActivationCodeSend(RequestMailRegistrationActivationCode mail, DbAuthUsers user);
    }
}

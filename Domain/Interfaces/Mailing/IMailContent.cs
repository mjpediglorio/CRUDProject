using Domain.Db.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Mailing
{
    public interface IMailContent
    {
        string GenerateHtml(DbAuthUsers user, string content);
        string EmailSubjectGet();
    }
}

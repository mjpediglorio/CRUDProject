using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Db.Request.Mailing
{
    public class RequestMailBase
    {
        public string[] Receiver { get; set; }
        public string ClientName { get; set; }
    }
}

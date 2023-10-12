using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class ResultBaseDto
    {
        [DataMember(Order = 98)]
        public int Code { get; set; }

        [DataMember(Order = 99)]
        public string? Message { get; set; }

        [DataMember(Order = 100)]
        public string? ErrorMsg { get; set; }
    }
}

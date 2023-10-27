using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common
{
    public record DbResult
    {
        [NotMapped]
        public DbResultTypes Result { get; set; }
    }
}

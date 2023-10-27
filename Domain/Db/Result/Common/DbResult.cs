using Domain.Db.Enums.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Db.Result.Common
{
    public record DbResult
    {
        [NotMapped]
        public DbResultTypes Result { get; set; }
    }
}

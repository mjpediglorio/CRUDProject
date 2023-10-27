using Domain.Db.Enums.Auth;
using Domain.Db.Result.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Db.Entities
{
    public record DbAuthUsers :DbResult
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string ContactNumber { get; set; }
        public string? ProfilePicture { get; set; }
        public int IsVerified { get; set; }
        public UserRolesType UserRoles { get; set; }
        public AuthStatusType Status { get; set; }
    }
}

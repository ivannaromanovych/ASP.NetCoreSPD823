using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Komunalka.Entities
{
    public class DbRole : IdentityRole<string>
    {
        public ICollection<DbUserRole> UserRoles { get; set; }
    }
}
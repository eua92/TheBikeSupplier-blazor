using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheBikeSupplier.Common;

namespace TheBikeSupplier.Models
{
    public class ApplicationRole : IdentityRole<Guid>
    {
    }

    public class ApplicationRoles
    {
        public readonly List<ApplicationRole> RoleList;
        public ApplicationRoles()
        {
            RoleList = new List<ApplicationRole>()
            {
                new ApplicationRole() { Name = Roles.Admin },
                new ApplicationRole() { Name = Roles.User },
                new ApplicationRole() { Name = Roles.Support }
            };
        }
    }
}

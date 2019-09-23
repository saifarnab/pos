using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace PosSustemUIU.ViewModels
{
    public class UserRoleVM
    {
        public IEnumerable<IdentityRole> Roles { get; set; }
        public string RoleName { get; set; }
    }
}
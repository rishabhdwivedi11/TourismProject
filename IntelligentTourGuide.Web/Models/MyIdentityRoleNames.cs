using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IntelligentTourGuide.Web.Migrations
{
    public enum MyIdentityRoleNames
    {
        [Display(Name = "Admin Role")]
        AppAdmin,

        [Display(Name = "User Role")]
        AppUser
    }
}

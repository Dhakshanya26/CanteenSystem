using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CanteenSystem.Dto.Models
{
    public class ApplicationUser : IdentityUser
    {
        public virtual ICollection<UserProfile> UserProfiles { get; set; }
    }

}

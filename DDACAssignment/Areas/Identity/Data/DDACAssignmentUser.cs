using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace DDACAssignment.Areas.Identity.Data
{
    // Add profile data for application users by adding properties to the DDACAssignmentUser class
    public class DDACAssignmentUser : IdentityUser
    {
        [PersonalData]
        public string Name { get; set; }   

        [PersonalData]
        public String Address { get; set; }

        [PersonalData]
        public String Role { get; set; }

    }
}

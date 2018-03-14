using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace BucBoard.Models
{

    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {

        public DateTime? LastLogin { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public string ClassSchedule { get; set; }

        public string HeadLine { get; set; }

        public byte[] ProfilePicture { get; set; }

        public IdentityRole Roles { get; set; }


    }
}

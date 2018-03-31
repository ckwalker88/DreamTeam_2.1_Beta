using System;
using System.Collections.Generic;

namespace BucBoard.Models.Entities.Existing
{
    public partial class AspNetUsers
    {
        public AspNetUsers()
        {
            Announcement = new HashSet<Announcement>();
            AspNetUserClaims = new HashSet<AspNetUserClaims>();
            AspNetUserLogins = new HashSet<AspNetUserLogins>();
            AspNetUserRoles = new HashSet<AspNetUserRoles>();
            AspNetUserTokens = new HashSet<AspNetUserTokens>();
            Course = new HashSet<Course>();
            DayOfWeek = new HashSet<DayOfWeek>();
            ProfilePictureNavigation = new HashSet<ProfilePicture>();
            Time = new HashSet<Time>();
        }

        public string Id { get; set; }
        public int AccessFailedCount { get; set; }
        public string ConcurrencyStamp { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public bool LockoutEnabled { get; set; }
        public DateTimeOffset? LockoutEnd { get; set; }
        public string NormalizedEmail { get; set; }
        public string NormalizedUserName { get; set; }
        public string PasswordHash { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public string SecurityStamp { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public string UserName { get; set; }
        public string ClassSchedule { get; set; }
        public string FirstName { get; set; }
        public string HeadLine { get; set; }
        public DateTime? LastLogin { get; set; }
        public string LastName { get; set; }
        public byte[] ProfilePicture { get; set; }
        public string RolesId { get; set; }

        public AspNetRoles Roles { get; set; }
        public ICollection<Announcement> Announcement { get; set; }
        public ICollection<AspNetUserClaims> AspNetUserClaims { get; set; }
        public ICollection<AspNetUserLogins> AspNetUserLogins { get; set; }
        public ICollection<AspNetUserRoles> AspNetUserRoles { get; set; }
        public ICollection<AspNetUserTokens> AspNetUserTokens { get; set; }
        public ICollection<Course> Course { get; set; }
        public ICollection<DayOfWeek> DayOfWeek { get; set; }
        public ICollection<ProfilePicture> ProfilePictureNavigation { get; set; }
        public ICollection<Time> Time { get; set; }
    }
}

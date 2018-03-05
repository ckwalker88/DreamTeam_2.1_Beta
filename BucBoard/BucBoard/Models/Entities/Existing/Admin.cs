using System;
using System.Collections.Generic;

namespace BucBoard.Models.Entities.Existing
{
    public partial class Admin
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public byte[] ProfilePicture { get; set; }
        public string ClassSchedule { get; set; }
        public string Headline { get; set; }
        public string Password { get; set; }
    }
}

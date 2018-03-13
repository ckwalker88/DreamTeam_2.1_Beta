using System;
using System.Collections.Generic;

namespace BucBoard.Models.Entities.Existing
{
    public partial class ProfilePicture
    {
        public int Id { get; set; }
        public byte[] Picture { get; set; }
        public string ApplicationUserId { get; set; }

        public AspNetUsers ApplicationUser { get; set; }
    }
}

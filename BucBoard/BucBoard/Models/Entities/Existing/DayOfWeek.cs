using System;
using System.Collections.Generic;

namespace BucBoard.Models.Entities.Existing
{
    public partial class DayOfWeek
    {
        public int Id { get; set; }
        public string Day { get; set; }
        public string ApplicationUserId { get; set; }

        public AspNetUsers ApplicationUser { get; set; }
    }
}

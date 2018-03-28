using System;
using System.Collections.Generic;

namespace BucBoard.Models.Entities.Existing
{
    public partial class Time
    {
        public int Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime StopTime { get; set; }
        public string ApplicationUserId { get; set; }

        public AspNetUsers ApplicationUser { get; set; }
    }
}

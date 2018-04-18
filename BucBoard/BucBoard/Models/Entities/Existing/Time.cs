using System;
using System.Collections.Generic;

namespace BucBoard.Models.Entities.Existing
{
    public partial class Time
    {
        public int Id { get; set; }
        public string StartTime { get; set; }
        public string StopTime { get; set; }
        public int ScheduleId { get; set; }

        public Schedule Schedule { get; set; }
    }
}

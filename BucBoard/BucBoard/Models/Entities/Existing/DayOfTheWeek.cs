using System;
using System.Collections.Generic;

namespace BucBoard.Models.Entities.Existing
{
    public partial class DayOfTheWeek
    {
        public int Id { get; set; }
        public string DayOfTheWeek1 { get; set; }
        public int ScheduleId { get; set; }

        public Schedule Schedule { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace BucBoard.Models.Entities.Existing
{
    public partial class Schedule
    {
        public Schedule()
        {
            Course = new HashSet<Course>();
            DayOfTheWeek = new HashSet<DayOfTheWeek>();
            Time = new HashSet<Time>();
        }

        public int Id { get; set; }
        public string ApplicationUserId { get; set; }

        public ICollection<Course> Course { get; set; }
        public ICollection<DayOfTheWeek> DayOfTheWeek { get; set; }
        public ICollection<Time> Time { get; set; }
    }
}

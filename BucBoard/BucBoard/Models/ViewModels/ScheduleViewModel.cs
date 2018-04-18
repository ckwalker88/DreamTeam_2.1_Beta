using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BucBoard.Models.ViewModels
{
    public class ScheduleViewModel
    {
        public string CourseCode { get; set; }
        public int CourseNumber { get; set; }
        public string CourseName { get; set; }
        public string DayOfTheWeek1 { get; set; }
        public string StartTime { get; set; }
        public string StopTime { get; set; }
    }
}

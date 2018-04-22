using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace BucBoard.Models.ViewModels
{
    public class ScheduleViewModel
    {
        [DisplayName("Course Code")]
        public string CourseCode { get; set; }
        [DisplayName("Course Number")]
        public int CourseNumber { get; set; }
        [DisplayName("Course Name")]
        public string CourseName { get; set; }
        [DisplayName("Days Of The Week")]
        public string DayOfTheWeek1 { get; set; }
        [DisplayName("Start Time")]
        public string StartTime { get; set; }
        [DisplayName("Stop Time")]
        public string StopTime { get; set; }
        public int ScheduleId { get; set; }
    }
}

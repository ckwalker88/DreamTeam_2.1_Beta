using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BucBoard.Models.Entities.Existing
{
    public partial class DayOfTheWeek
    {
        public int Id { get; set; }
        [Display(Name = "Day Of The Week") ]
        public string DayOfTheWeek1 { get; set; }
        public string ApplicationUserId { get; set; }

        public AspNetUsers ApplicationUser { get; set; }
    }
}

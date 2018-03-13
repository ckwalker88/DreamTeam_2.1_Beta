using System;
using System.Collections.Generic;

namespace BucBoard.Models.Entities.Existing
{
    public partial class ClassSchedule
    {
        public int Id { get; set; }
        public string Schedule { get; set; }
        public string ApplicationUserId { get; set; }

        public AspNetUsers ApplicationUser { get; set; }
    }
}

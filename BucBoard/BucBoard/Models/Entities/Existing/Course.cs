﻿using System;
using System.Collections.Generic;

namespace BucBoard.Models.Entities.Existing
{
    public partial class Course
    {
        public int Id { get; set; }
        public string ApplicationUserId { get; set; }
        public string CourseCode { get; set; }
        public int CourseNumber { get; set; }
        public string CourseName { get; set; }

        public AspNetUsers ApplicationUser { get; set; }
    }
}

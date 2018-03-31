using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BucBoard.Models.ViewModels
{
    public class EmailViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string StudentEmail { get; set; }

        [Required]
        [Display(Name = "Subject")]
        public string Subject { get; set; }

        [Required]
        [Display(Name = "Message")]
        public string Message { get; set; }
    }
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BucBoard.Models;
using BucBoard.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BucBoard.Controllers
{
    public class ScheduleController : Controller
    {
        private ICourseRepository _courseRepository;
        private IDayOfTheWeekRepository _dayWeekRepository;
        private ITimeRepository _timeRepository;
        private UserManager<ApplicationUser> _userManager;

        public ScheduleController(ICourseRepository courseRepository, IDayOfTheWeekRepository dayWeekRepository, ITimeRepository timeRepository, UserManager<ApplicationUser> userManager)
        {
            _courseRepository = courseRepository;
            _dayWeekRepository = dayWeekRepository;
            _timeRepository = timeRepository;
            _userManager = userManager;
        }
        public IActionResult Create()
        {
            return View();
        }
    }
}
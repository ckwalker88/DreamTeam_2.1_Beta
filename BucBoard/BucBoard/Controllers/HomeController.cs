using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BucBoard.Models;
using BucBoard.Services.Interfaces;
using BucBoard.Models.Entities.Existing;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace BucBoard.Controllers
{
    [Authorize(Roles = "SuperAdmin, Admin")]
    public class HomeController : Controller
    {
        private ICourseRepository _courseRepository;
        private IAnnouncementRepository _announcementRepo;
        private UserManager<ApplicationUser> _userManager;

        public HomeController(ICourseRepository courseRepository, IAnnouncementRepository announcementRepo, UserManager<ApplicationUser> userManager)
        {
            _courseRepository = courseRepository;
            _announcementRepo = announcementRepo;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            ViewBag.UserId = _userManager.GetUserId(HttpContext.User);

            var announcements = _announcementRepo.ReadAllAnnouncements();
            var query = announcements.Where(a => a.ApplicationUserId == ViewBag.UserId);
            var model = query.ToList();

            var courses = _courseRepository.ReadAllCourses();
            var query2 = courses.Where(c => c.ApplicationUserId == ViewBag.UserId);
            var model2 = query2.ToList();

            ViewBag.courseList = model2;
            return View(model);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

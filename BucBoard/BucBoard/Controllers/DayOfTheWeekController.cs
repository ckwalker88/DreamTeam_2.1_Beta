using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BucBoard.Models;
using BucBoard.Models.Entities.Existing;
using BucBoard.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BucBoard.Controllers
{
    [Authorize(Roles = "SuperAdmin, Admin")]
    public class DayOfTheWeekController : Controller
    {

        private IDayOfTheWeekRepository _day;
        private UserManager<ApplicationUser> _userManager;

        public DayOfTheWeekController(IDayOfTheWeekRepository day, UserManager<ApplicationUser> user)
        {
            _day = day;
            _userManager = user;
        }

        public IActionResult Index()
        {
            return View(_day.ReadAll());
        }

        public IActionResult Create()
        {
            ViewBag.userId = _userManager.GetUserId(HttpContext.User);
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Create(DayOfTheWeek day)
        {
            if (ModelState.IsValid)
            {
                _day.Create(day);
                return RedirectToAction("Index");
            }
            
            return View();
        }

        
    }
}
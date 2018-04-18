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

        public IActionResult Edit(int id)
        {
            var day = _day.Read(id);
            if (day == null)
            {
                return RedirectToAction("Index");
            }
            return View(day);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Edit(DayOfTheWeek day)
        {
            if (ModelState.IsValid)
            {
                _day.Update(day.Id, day);
                return RedirectToAction("Index");
            }
            return View(day);
        }

        public IActionResult Details(int id)
        {
            var day = _day.Read(id);
            if (day == null)
            {
                return RedirectToAction("Index");
            }
            return View(day);
        }

        public IActionResult Delete(int id)
        {
            var day = _day.Read(id);
            if (day == null)
            {
                return RedirectToAction("Index");
            }
            return View(day);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            _day.Delete(id);
            return RedirectToAction("Index");

        }

    }
}
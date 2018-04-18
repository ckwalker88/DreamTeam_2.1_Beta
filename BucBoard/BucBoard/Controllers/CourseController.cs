using BucBoard.Models;
using BucBoard.Models.Entities.Existing;
using BucBoard.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BucBoard.Controllers
{
    [Authorize(Roles = "SuperAdmin, Admin")]
    public class CourseController : Controller
    {
        private ICourseRepository _repo;
        private UserManager<ApplicationUser> _userManager;

        public CourseController(ICourseRepository repo, UserManager<ApplicationUser> userManager)
        {
            _repo = repo;
            _userManager = userManager;
        }

        public IActionResult Index()
        {

            return View(_repo.ReadAllCourses());
        }

        public IActionResult Create()
        {
            ViewBag.userId = _userManager.GetUserId(HttpContext.User);
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Create(Course course)
        {
            if (ModelState.IsValid)
            {
                _repo.CreateCourse(course);
                return RedirectToAction("Index", "Course");
            }
            return View();
        }

        public IActionResult Details(int id)
        {
            var course = _repo.ReadCourse(id);
            if (course == null)
            {
                return RedirectToAction("Index");
            }
            return View(course);
        }

        public IActionResult Edit(int id)
        {
            var course = _repo.ReadCourse(id);
            if (course == null)
            {
                return RedirectToAction("Index", "Course");
            }
            return View(course);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Edit(Course course)
        {
            if (ModelState.IsValid)
            {
                _repo.UpdateCourse(course.Id, course);
                return RedirectToAction("Index", "Course");
            }
            return View(course);
        }

        public IActionResult Delete(int id)
        {
            var course = _repo.ReadCourse(id);
            if (course == null)
            {
                return RedirectToAction("Index", "Course");
            }
            return View(course);
        }

        [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int Id)
        {
            _repo.DeleteCourse(Id);
            return RedirectToAction("Index", "Course");
        }
    }
}
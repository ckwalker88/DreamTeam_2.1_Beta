using BucBoard.Models;
using BucBoard.Models.Entities.Existing;
using BucBoard.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


namespace BucBoard.Controllers
{
    public class TimeController : Controller
	{
		private UserManager<ApplicationUser> _userManager;

		private ITimeRepository _repo;

		public TimeController(ITimeRepository repo, UserManager<ApplicationUser> userManager)
		{
			_repo = repo;
			_userManager = userManager;
		}

		[Authorize(Roles = "SuperAdmin, Admin")]
		public IActionResult Index()
		{

			return View(_repo.ReadAllTime());
		}

		[Authorize(Roles = "SuperAdmin, Admin")]
		public IActionResult Create()
		{
			ViewBag.userId = _userManager.GetUserId(HttpContext.User);
			return View();
		}

		[Authorize(Roles = "SuperAdmin, Admin")]
		[HttpPost, ValidateAntiForgeryToken]
		public IActionResult Create(Time time)
		{
			if (ModelState.IsValid)
			{
				_repo.CreateTime(time);
				return RedirectToAction("Index", "Time");
			}
			return View();
		}

		[Authorize(Roles = "SuperAdmin, Admin")]
		public IActionResult Details(int id)
		{
			var time = _repo.ReadTime(id);
			if (time == null)
			{
				return RedirectToAction("Index");
			}
			return View(time);
		}

		[Authorize(Roles = "SuperAdmin, Admin")]
		public IActionResult Edit(int id)
		{
			var time = _repo.ReadTime(id);
			if (time == null)
			{
				return RedirectToAction("Index", "Announcement");
			}
			return View(time);
		}

		[Authorize(Roles = "SuperAdmin, Admin")]
		[HttpPost, ValidateAntiForgeryToken]
		public IActionResult Edit(Time time)
		{
			if (ModelState.IsValid)
			{
				_repo.UpdateTime(time.Id, time);
				return RedirectToAction("Index", "Time");
			}
			return View(time);
		}

		[Authorize(Roles = "SuperAdmin, Admin")]
		public IActionResult Delete(int id)
		{
			var time = _repo.ReadTime(id);
			if (time == null)
			{
				return RedirectToAction("Index", "Time");
			}
			return View(time);
		}

		[Authorize(Roles = "SuperAdmin, Admin")]
		[HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
		public IActionResult DeleteConfirmed(int Id)
		{
			_repo.DeleteTime(Id);
			return RedirectToAction("Index", "Time");
		}
	}
}
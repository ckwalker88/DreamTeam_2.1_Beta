using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BucBoard.Models;
using BucBoard.Models.Entities.Existing;
using BucBoard.Models.ViewModels;
using BucBoard.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BucBoard.Controllers
{


    public class AnnouncementController : Controller
    {
        private UserManager<ApplicationUser> _userManager;

        private IAnnouncementRepository _repo;

        public AnnouncementController(IAnnouncementRepository repo, UserManager<ApplicationUser> userManager)
        {
            _repo = repo;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
           
            return View(_repo.ReadAllAnnouncements());
        }

        public IActionResult Create()
        {
            ViewBag.userId = _userManager.GetUserId(HttpContext.User);
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Create(Announcement announcement)
        {
            if (ModelState.IsValid)
            {
                _repo.CreateAnnouncement(announcement);
                return RedirectToAction("Index", "Announcement");
            }
            return View();
        }

        public IActionResult Details(int id)
        {
            var announcement = _repo.ReadAnnouncement(id);
            if (announcement == null)
            {
                return RedirectToAction("Index");
            }
            return View(announcement);
        }

        public IActionResult Edit(int id)
        {
            var announcement = _repo.ReadAnnouncement(id);
            if (announcement == null)
            {
                return RedirectToAction("Index", "Announcement");
            }
            return View(announcement);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Edit(Announcement announcement)
        {
            if (ModelState.IsValid)
            {
                _repo.UpdateAnnouncement(announcement.Id, announcement);
                return RedirectToAction("Index", "Announcement");
            }
            return View(announcement);
        }

        public IActionResult Delete(int id)
        {
            var announcement = _repo.ReadAnnouncement(id);
            if (announcement == null)
            {
                return RedirectToAction("Index", "Announcement");
            }
            return View(announcement);
        }

        [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int Id)
        {
            _repo.DeleteAnnouncement(Id);
            return RedirectToAction("Index", "Announcement");
        }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BucBoard.Models.Entities.Existing;
using BucBoard.Models.ViewModels;
using BucBoard.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BucBoard.Controllers
{


    public class AnnouncementController : Controller
    {

        private IAnnouncementRepository _repo;

        public AnnouncementController(IAnnouncementRepository repo)
        {
            _repo = repo;
        }

        public IActionResult Index()
        {
            //var model = _repo.ReadAllAnnouncements()
            //    .Select(a => new AnnounceIndexVM
            //    {
            //        message = a.Message
            //    });

            //var messages = _repo.ReadAllAnnouncements();
            //var users = _repo.

            //var query = from an in _repo.ReadAllAnnouncements()
            //            where an.


            return View(_repo.ReadAllAnnouncements());
        }

        public IActionResult Create()
        {
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

    }
}
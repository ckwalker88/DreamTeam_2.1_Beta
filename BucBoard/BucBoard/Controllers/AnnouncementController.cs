using BucBoard.Models;
using BucBoard.Models.Entities.Existing;
using BucBoard.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace BucBoard.Controllers
{

    [Authorize(Roles = "SuperAdmin, Admin")]
    public class AnnouncementController : Controller
    {
        //inject necessary repositories
        private UserManager<ApplicationUser> _userManager;

        private IAnnouncementRepository _repo;

        //constructor
        public AnnouncementController(IAnnouncementRepository repo, UserManager<ApplicationUser> userManager)
        {
            _repo = repo;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            //get user id of currently logged in user and pass it through the ViewBag
            ViewBag.UserId = _userManager.GetUserId(HttpContext.User);

            //var msgs = _repo.ReadAllAnnouncements();
            
            //var announcement = msgs.Where(p => p.ApplicationUserId == ViewBag.UserId).FirstOrDefault().Message;
                
            return View(_repo.ReadAllAnnouncements().Where(p => p.ApplicationUserId == ViewBag.UserId)); //return all announcements of currently logged in user
        }

        public IActionResult Create()
        {
            //get current logged in user's id and pass it through the ViewBag
            ViewBag.userId = _userManager.GetUserId(HttpContext.User);
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Create(Announcement announcement)
        {
            if (ModelState.IsValid)
            {
                //create announcement and then reidrect back to the announcement index page
                _repo.CreateAnnouncement(announcement);
                return RedirectToAction("Index", "Announcement");
            }
            return View();
        }

        public IActionResult Edit(int id)
        {
            //pull announcement from database
            var announcement = _repo.ReadAnnouncement(id);
            if (announcement == null)
            {
                //if null, redirect back to announcement index
                return RedirectToAction("Index", "Announcement");
            }
            return View(announcement);//pass announcement to the view
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Edit(Announcement announcement)//pull in user's changes through the "announcement" object parameter
        {
            if (ModelState.IsValid)
            {
                //update the announcement from the user's changes and then redirect to the announcement index
                _repo.UpdateAnnouncement(announcement.Id, announcement);
                return RedirectToAction("Index", "Announcement");
            }
            return View(announcement);
        }

        public IActionResult Delete(int id)
        {
            //pull announcement from database
            var announcement = _repo.ReadAnnouncement(id);
            if (announcement == null)
            {
                //if null, redirect back to the announcement index
                return RedirectToAction("Index", "Announcement");
            }
            return View(announcement);
        }

        [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int Id)//pull in id of announcement to be deleted from the view
        {
            //delete the announcement and redirect back to the announcement index
            _repo.DeleteAnnouncement(Id);
            return RedirectToAction("Index", "Announcement");
        }

    }
}
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BucBoard.Models;
using BucBoard.Services.Interfaces;
using BucBoard.Models.Entities.Existing;

namespace BucBoard.Controllers
{
    public class HomeController : Controller
    {
        private IAnnouncementRepository _repo;

        public HomeController(IAnnouncementRepository repo)
        {
            _repo = repo;
        }
        public IActionResult Index()
        {
            return View(_repo.ReadAllAnnouncements());
        }

        public IActionResult Email()
        {
            return View();
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

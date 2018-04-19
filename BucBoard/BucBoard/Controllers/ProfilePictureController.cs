using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BucBoard.Models;
using BucBoard.Models.Entities.Existing;
using BucBoard.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BucBoard.Controllers
{
    [Authorize (Roles = "SuperAdmin, Admin")]
    public class ProfilePictureController : Controller
    {

        private IProfilePictureRepository _pic;
        private UserManager<ApplicationUser> _userManager;

        public ProfilePictureController(IProfilePictureRepository pic, UserManager<ApplicationUser> user)
        {
            _pic = pic;
            _userManager = user;
        }

        public IActionResult Index()
        {
            ViewBag.UserId = _userManager.GetUserId(HttpContext.User);

            var pics = _pic.SeeAllPictures();

            try
            {

                var userPic = pics.Where(p => p.ApplicationUserId == ViewBag.UserId).FirstOrDefault().Picture;

                string convertToBase64 = Convert.ToBase64String(userPic);
                string imgDataURL = string.Format("data:image/png;base64,{0}", convertToBase64);

                ViewBag.DisplayImage = imgDataURL;
            }
            catch (Exception)
            {
                string imgPath = $"{Directory.GetCurrentDirectory()}{@"\wwwroot\images\avatar.png"}";
                byte[] byteData = System.IO.File.ReadAllBytes(imgPath);
                string base64data = Convert.ToBase64String(byteData);
                string imgDataURL = string.Format("data:image/png;base64,{0}", base64data);
                ViewBag.DisplayImage = imgDataURL;
            }   
                return View(_pic.SeeAllPictures().Where(p => p.ApplicationUserId == ViewBag.UserId));
        }

        public IActionResult Create()
        {
            ViewBag.UserId = _userManager.GetUserId(HttpContext.User);
            return View();
        }

        [HttpPost]
        public IActionResult CreatePicture(ProfilePicture profilePicture, IFormFile pic)
        {
            ViewBag.UserId = _userManager.GetUserId(HttpContext.User);
            if (ModelState.IsValid)
            {
                if (_pic.SeeAllPictures().Where(p => p.ApplicationUserId == ViewBag.UserId).Count() >= 1)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    using (var ms = new MemoryStream())
                    {
                        pic.OpenReadStream().CopyTo(ms);

                        profilePicture.Picture = ms.ToArray();
                        _pic.UploadPicture(profilePicture);

                        return RedirectToAction("Index");

                    }
                }
                
                
            }
            return View();
        }

        public IActionResult Details(int id)
        {
            var picture = _pic.GetPic(id);
            if (picture == null)
            {
                return RedirectToAction("Index");
            }
            return View(picture);
        }

        public IActionResult Delete(int id)
        {

            ViewBag.UserId = _userManager.GetUserId(HttpContext.User);

            var pics = _pic.SeeAllPictures();

            var userPic = pics.Where(p => p.ApplicationUserId == ViewBag.UserId).FirstOrDefault().Picture;

            string convertToBase64 = Convert.ToBase64String(userPic);
            string imgDataURL = string.Format("data:image/png;base64,{0}", convertToBase64);

            ViewBag.DisplayImage = imgDataURL;
            var pic = _pic.GetPic(id);
            if (pic == null)
            {
                return RedirectToAction("Index");
            }
            return View(pic);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            _pic.DeletePicture(id);
            return RedirectToAction("Index");
        }

    }
}
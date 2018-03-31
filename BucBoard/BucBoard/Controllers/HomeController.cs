using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BucBoard.Models;
using BucBoard.Services.Interfaces;
using BucBoard.Models.Entities.Existing;
using Microsoft.AspNetCore.Authorization;
using BucBoard.Models.ViewModels;
using System.Net.Mail;
using System.Net;

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

        [HttpGet]
        //[AllowAnonymous]
        public ActionResult Email()
        {
            
            return View();
        }

        [HttpPost]
        public ActionResult Email(string studentEmail, string emailSubject, string emailMessage)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    var senderemail = new MailAddress("email@gmail.com", "Test"); // this is the sender, ill make a dummy email instead of what's here
                    var receiveremail = new MailAddress("chaseday95@gmail.com", "Receiver"); // this is who receives the email, will probably be the professor's email.

                    var password = "demotest123"; // test email's password
                    var sub = emailSubject + " From: " + studentEmail; //  this is the subject line
                    var body = emailMessage; // obvious

                    var smtp = new SmtpClient // setup the SMTP client here
                    {
                        Host = "smtp.gmail.com",
                        Port = 587,
                        EnableSsl = true,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        UseDefaultCredentials = false,
                        Credentials = new NetworkCredential(senderemail.Address, password)
                    };

                    using (var mess = new MailMessage(senderemail, receiveremail)
                    {
                        Subject = emailSubject,
                        Body = emailMessage
                    }
                    )
                    {
                        smtp.Send(mess);
                    }

                    return View();
                }

            }
            catch(Exception)
            {
                ViewBag.Error = "Wrench in cogs";
            }

            return View();
        }
        

        //[HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Email(EmailViewModel model, string returnUrl = null)
        //{
        //    ViewData["ReturnUrl"] = returnUrl;
        //    if (ModelState.IsValid)
        //    {
        //        var user = new ApplicationUser { UserName = model.Email, Email = model.Email, FirstName = model.FirstName, LastName = model.LastName };
        //        var result = await _userManager.CreateAsync(user, model.Password);
        //        if (result.Succeeded)
        //        {
        //            _logger.LogInformation("User created a new account with password.");

        //            //Add a user to the default role, or any role we specify 
        //            await _userManager.AddToRoleAsync(user, "Admin");

        //            //Add the role "Admin" Id to the RolesId column in the AspNetUsers table 
        //            string role = "c2181b3b-f186-4439-bbba-9a0a68585791";

        //            //Get the users id for the query below
        //            string Id = await _userManager.GetUserIdAsync(user);



        //            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        //            var callbackUrl = Url.EmailConfirmationLink(user.Id, code, Request.Scheme);
        //            await _emailSender.SendEmailConfirmationAsync(model.Email, callbackUrl);

        //            await _signInManager.SignInAsync(user, isPersistent: false);
        //            _logger.LogInformation("User created a new account with password.");
        //            return RedirectToLocal(returnUrl);
        //        }
        //        AddErrors(result);
        //    }

        //    // If we got this far, something failed, redisplay form
        //    return View(model);
        //}

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

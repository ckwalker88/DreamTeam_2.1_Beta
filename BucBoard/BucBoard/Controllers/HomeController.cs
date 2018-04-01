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


        /*
         * Don't touch this or it will break the email send server.
         * 
         * Don't modify unless your cucumber leaves bloom scarlet.
         * 
         * Love ya - D
         * 
         */

        [HttpPost]
        public ActionResult Email(string studentEmail, string emailSubject, string emailMessage)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    var senderemail = new MailAddress("bucboardlistserv@gmail.com", "Buc Board"); // this is the sender, ill make a dummy email instead of what's here
                    var receiveremail = new MailAddress("chaseday95@gmail.com", "Receiver"); // this is who receives the email, will probably be the professor's email.

                    var password = "PussyFucker69#Dongs"; // test email's password
                    var sub = "Buc Board Email - From: " + studentEmail + " " + emailSubject; //  this is the subject line
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

                    using (var message = new MailMessage(senderemail, receiveremail)
                    {
                        Subject = sub,
                        Body = body
                    }
                    )
                    {
                        smtp.Send(message);
                    }

                    ViewBag.Success = "Email succesfully sent.";
                    return View();
                }

            }
            catch(Exception)
            {
                ViewBag.Error = "Wrench in cogs";
            }

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

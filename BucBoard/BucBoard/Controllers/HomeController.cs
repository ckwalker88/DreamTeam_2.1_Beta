using System;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using BucBoard.Models;
using BucBoard.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using System.Net.Mail;
using System.Net;


namespace BucBoard.Controllers
{
    //son_of_anarchy - change
    [Authorize(Roles = "SuperAdmin, Admin")]
    public class HomeController : Controller
    {
        private ICourseRepository _courseRepository;
        private IDayOfTheWeekRepository _dayOfTheWeekRepository;
        private ITimeRepository _timeRepository;
        private IAnnouncementRepository _announcementRepo;
        private UserManager<ApplicationUser> _userManager;

        public HomeController(ICourseRepository courseRepository, 
                              IDayOfTheWeekRepository dayOfTheWeekRepository,
                              ITimeRepository timeRepository,
                              IAnnouncementRepository announcementRepo, 
                              UserManager<ApplicationUser> userManager)
        {
            _courseRepository = courseRepository;
            _dayOfTheWeekRepository = dayOfTheWeekRepository;
            _timeRepository = timeRepository;
            _announcementRepo = announcementRepo;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            ViewBag.UserId = _userManager.GetUserId(HttpContext.User);

            var announcements = _announcementRepo.ReadAllAnnouncements();
            var query = announcements.Where(a => a.ApplicationUserId == ViewBag.UserId);
            var model = query.ToList();

            //var courses = _courseRepository.ReadAllCourses();
            //var query2 = courses.Where(c => c.ApplicationUserId == ViewBag.UserId);
            //var model2 = query2.ToList();

            //var days = _dayOfTheWeekRepository.ReadAll();
            //var query3 = days.Where(c => c.ApplicationUserId == ViewBag.UserId);
            //var model3 = query3.ToList();

            //var times = _timeRepository.ReadAllTime();
            //var query4 = times.Where(c => c.ApplicationUserId == ViewBag.UserId);
            //var model4 = query4.ToList();





            //ViewBag.courseList = model2;
            //ViewBag.dayList = model3;
            //ViewBag.timeList = model4;
            //return View(model);
            return View();
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
            catch (Exception)
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

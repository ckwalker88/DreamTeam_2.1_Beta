using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BucBoard.Models;
using BucBoard.Services.Interfaces;
using BucBoard.Models.Entities.Existing;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using System.Net.Mail;
using System.Net;
using Microsoft.AspNetCore.Http;
using System.IO;
using BucBoard.Models.ViewModels;

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
        private IProfilePictureRepository _pic;
        private IAuthenticationRepository _user;
        private IScheduleRepository _scheduleRepository;

        public HomeController(ICourseRepository courseRepository,
                              IDayOfTheWeekRepository dayOfTheWeekRepository,
                              ITimeRepository timeRepository,
                              IAnnouncementRepository announcementRepo,
                              UserManager<ApplicationUser> userManager,
                              IProfilePictureRepository pic,
                              IAuthenticationRepository user,
                              IScheduleRepository scheduleRepository)
        {
            _courseRepository = courseRepository;
            _dayOfTheWeekRepository = dayOfTheWeekRepository;
            _timeRepository = timeRepository;
            _announcementRepo = announcementRepo;
            _userManager = userManager;
            _pic = pic;
            _user = user;
            _scheduleRepository = scheduleRepository;
           
        }
        public IActionResult Index()
        {
            ViewBag.UserId = _userManager.GetUserId(HttpContext.User);

            var announcements = _announcementRepo.ReadAllAnnouncements();
            var query = announcements.Where(a => a.ApplicationUserId == ViewBag.UserId);
            var model = query.ToList();

            var courses = _courseRepository.ReadAllCourses();
            //var query2 = courses.Where(c => c.ApplicationUserId == ViewBag.UserId);
            //var model2 = query2.ToList();

            var days = _dayOfTheWeekRepository.ReadAll();
            //var query3 = days.Where(c => c.ApplicationUserId == ViewBag.UserId);
            //var model3 = query3.ToList();

            var times = _timeRepository.ReadAllTime();
            //var query4 = times.Where(c => c.ApplicationUserId == ViewBag.UserId);
            //var model4 = query4.ToList();

            var pics = _pic.SeeAllPictures();
            
            try
            {
                var userPic = pics.Where(p => p.ApplicationUserId == ViewBag.UserId).FirstOrDefault().Picture;
            
                var userName = _userManager.GetUserName(HttpContext.User);
                var users = _user.ReadAllUsers();
                var user = _user.Read(_userManager.GetUserId(HttpContext.User));
                var profName = user.FirstName + " " + user.LastName;
                //var name = users.Where(u => u.Id == ViewBag.UserId).F
                //var model5 = userPic.ToList();

                //var datPic = pics.


                string convertToBase64 = Convert.ToBase64String(userPic);
                string imgDataURL = string.Format("data:image/png;base64,{0}", convertToBase64);

                ViewBag.DisplayImage = imgDataURL;
                ViewBag.UserName = userName;
                ViewBag.DisplayName = profName;

                    //ViewBag.courseList = model2;
                    //ViewBag.dayList = model3;
                    //ViewBag.timeList = model4;
                    //ViewBag.userPictures = model5;
                    //ViewBag.profPic = userPic; 

                }
                catch (Exception)
                {
                    string imgPath = $"{Directory.GetCurrentDirectory()}{@"\wwwroot\images\avatar.png"}";
                    byte[] byteData = System.IO.File.ReadAllBytes(imgPath);
                    string base64data = Convert.ToBase64String(byteData);
                    string imgDataURL = string.Format("data:image/png;base64,{0}", base64data);
                    ViewBag.DisplayImage = imgDataURL;

                    var userName = _userManager.GetUserName(HttpContext.User);
                    var users = _user.ReadAllUsers();
                    var user = _user.Read(_userManager.GetUserId(HttpContext.User));
                    var profName = user.FirstName + " " + user.LastName;

                    ViewBag.UserName = userName;
                    ViewBag.DisplayName = profName;

            }

            //from here down is the Schedule Model
            var applicationUserId = _userManager.GetUserId(HttpContext.User);

            var scheduleList = _scheduleRepository.ReadAllSchedules().Where(s => s.ApplicationUserId == applicationUserId);

            ICollection<ScheduleViewModel> scheduleViewModelList = new List<ScheduleViewModel>();

            Course course = new Course();
            DayOfTheWeek dayOfTheWeek = new DayOfTheWeek();
            Time time = new Time();

            foreach (var schedule in scheduleList)
            {
                course = schedule.Course.FirstOrDefault(s => s.ScheduleId == schedule.Id);
                dayOfTheWeek = schedule.DayOfTheWeek.FirstOrDefault(d => d.ScheduleId == schedule.Id);
                time = schedule.Time.FirstOrDefault(t => t.ScheduleId == schedule.Id);

                scheduleViewModelList.Add(new ScheduleViewModel
                {
                    CourseCode = course.CourseCode,
                    CourseNumber = course.CourseNumber,
                    CourseName = course.CourseName,
                    DayOfTheWeek1 = dayOfTheWeek.DayOfTheWeek1,
                    StartTime = time.StartTime,
                    StopTime = time.StopTime,
                    ScheduleId = schedule.Id
                });
            }
            ViewBag.scheduleViewModelList = scheduleViewModelList;
            //end of Schedule Model

            return View(model);
        }

        /*
         * We could use this later for uploading files if we needed to
         */ 
        [HttpPost]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return Content("File not selected");
            }

            var path = Path.Combine(
                Directory.GetCurrentDirectory(), "wwwroot",
                file.Name);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return RedirectToAction("Index");
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
        public ActionResult Email(string appointment, string studentEmail, string emailSubject, string emailMessage)
        {   
            try
            {
                if (ModelState.IsValid)
                {
                    var senderemail = new MailAddress("bucboardlistserv@gmail.com", "Buc Board"); // this is the sender
                    var receiveremail = new MailAddress(User.Identity.Name, "Receiver"); // this is who receives the email, will probably be the professor's email.

                    var password = "PussyFucker69#Dongs"; // test email's password

                    var sub ="";
                    var body ="";

                    if (appointment == "on")
                    {
                        sub = "Buc Board Appointment Request - From: " + studentEmail + " " + emailSubject; //  this is the subject line
                        body = "\n\n\nThis student would like to schedule an appointment with you!\n\n" + emailMessage; // obvious
                    }
                    else
                    {
                        sub = "Buc Board Email - From: " + studentEmail + " " + emailSubject; //  this is the subject line
                        body = emailMessage; // obvious
                    }
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

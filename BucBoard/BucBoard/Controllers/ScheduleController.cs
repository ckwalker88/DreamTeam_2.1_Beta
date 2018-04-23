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
    public class ScheduleController : Controller
    {
        //inject all necessary repositories
        private ICourseRepository _courseRepository;
        private IDayOfTheWeekRepository _dayWeekRepository;
        private ITimeRepository _timeRepository;
        private UserManager<ApplicationUser> _userManager;
        private IScheduleRepository _scheduleRepository;

        //constructor
        public ScheduleController(IScheduleRepository scheduleRepository, UserManager<ApplicationUser> userManager, ICourseRepository courseRepository, IDayOfTheWeekRepository dayWeekRepository, ITimeRepository timeRepository)
        {
            _scheduleRepository = scheduleRepository;
            _userManager = userManager;
            _courseRepository = courseRepository;
            _dayWeekRepository = dayWeekRepository;
            _timeRepository = timeRepository;
        }

        public IActionResult Index()
        {
            //get user id of currently logged in user
            var applicationUserId = _userManager.GetUserId(HttpContext.User);

            //create a list of all schedules from the currently logged in user
            var scheduleList = _scheduleRepository.ReadAllSchedules().Where(s => s.ApplicationUserId == applicationUserId);

            //create an ICollection to hold ScheduleViewModels to be passed through to the view
            ICollection<ScheduleViewModel> scheduleViewModelList = new List<ScheduleViewModel>();


            //declare and initialize new objects of types Course, DayOfTheWeek, and Time
            Course course = new Course();
            DayOfTheWeek dayOfTheWeek = new DayOfTheWeek();
            Time time = new Time();

            //loop through each schedule in the list of schedules that we created above
            foreach(var schedule in scheduleList)
            {
                //grab the course information out of the schedule
                course = schedule.Course.FirstOrDefault(s => s.ScheduleId == schedule.Id);

                //grab the dayoftheweek information out of the schedule
                dayOfTheWeek = schedule.DayOfTheWeek.FirstOrDefault(d => d.ScheduleId == schedule.Id);

                //grab the time information out of the schedule
                time = schedule.Time.FirstOrDefault(t => t.ScheduleId == schedule.Id);

                //create a new ScheduleViewModel, fill it with the information that we pulled from the schedule, and add it to the scheduleViewModelList
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

            return View(scheduleViewModelList);//return a list of ScheduleViewModels
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Create(string courseCode, int courseNumber, string courseName, string dayOfTheWeek1, string startTime, string stopTime)
        {
            //get id of currently logged in user
            var applicationUserId = _userManager.GetUserId(HttpContext.User);

            //create new a schedule and tie it to the currently logged in user
            var schedule = new Schedule { ApplicationUserId = applicationUserId };

            //store the newly created schedule in the database
            _scheduleRepository.CreateSchedule(schedule);

            //create a new Course, DayOfTheWeek, and Time, fill them with the parameters passed in from the view (user input), and store them in the database
            _courseRepository.CreateCourse(new Course { CourseCode = courseCode, CourseNumber = courseNumber, CourseName = courseName, ScheduleId = schedule.Id});
            _dayWeekRepository.Create(new DayOfTheWeek { DayOfTheWeek1 = dayOfTheWeek1, ScheduleId = schedule.Id });
            _timeRepository.CreateTime(new Time { StartTime = startTime, StopTime = stopTime, ScheduleId = schedule.Id});

            //redirect back to the schedul index
            return RedirectToAction("Index", "Schedule");
        }

        public IActionResult Edit(int id)
        {
            //pull schedule (from id passed in as a parameter) and all it's associated data from the Course, DayOfTheWeek, and Time tables
            var schedule = _scheduleRepository.ReadSchedule(id);
            var course = schedule.Course.FirstOrDefault(s => s.ScheduleId == schedule.Id);
            var dayOfTheWeek = schedule.DayOfTheWeek.FirstOrDefault(d => d.ScheduleId == schedule.Id);
            var time = schedule.Time.FirstOrDefault(t => t.ScheduleId == schedule.Id);

            //create a new EditScheduleViewModel and fill it with the information collected from the database above
            var editScheduleViewModel = new EditScheduleViewModel
            {
                CourseCode = course.CourseCode,
                CourseNumber = course.CourseNumber,
                CourseName = course.CourseName,
                DayOfTheWeek1 = dayOfTheWeek.DayOfTheWeek1,
                StartTime = time.StartTime,
                StopTime = time.StopTime,
                ScheduleId = schedule.Id,
                CourseId = course.Id,
                DayOfTheWeekId = dayOfTheWeek.Id,
                TimeId = time.Id
            };

            //pass this EditScheduleViewModel to the user, which includes the information of the schedule to be editted
            return View(editScheduleViewModel);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Edit(string courseCode, int courseNumber, string courseName, string dayOfTheWeek1, string startTime, string stopTime, int scheduleId,
                                   int courseId, int dayOfTheWeekId, int timeId)
        {
            //create a new Course from the user input changes passed in from the view
            Course course = new Course
            {
                Id = courseId,
                CourseCode = courseCode,
                CourseNumber = courseNumber,
                CourseName = courseName,
                ScheduleId = scheduleId
            };

            //create a DayOfTheWeek from the user input changes passed in from the view
            DayOfTheWeek dayOfTheWeek = new DayOfTheWeek
            {
                Id = dayOfTheWeekId,
                DayOfTheWeek1 = dayOfTheWeek1,
                ScheduleId = scheduleId
            };

            //create a new Time from the user input changes passed in from the view
            Time time = new Time
            {
                Id = timeId,
                StartTime = startTime,
                StopTime = stopTime,
                ScheduleId = scheduleId
            };

            //store each of the individually created schedule parts from above in the database
            _courseRepository.UpdateCourse(course);
            _dayWeekRepository.Update(dayOfTheWeek);
            _timeRepository.UpdateTime(time);

            return RedirectToAction("Index", "Schedule");
        }

        public IActionResult Delete(int id)
        {
            //pull in schedule from the database (from passed in id parameter), and all it's associated data from the Course, DayOfTheWeek, and Time tables
            var schedule = _scheduleRepository.ReadSchedule(id);
            var course = schedule.Course.FirstOrDefault(s => s.ScheduleId == schedule.Id);
            var dayOfTheWeek = schedule.DayOfTheWeek.FirstOrDefault(d => d.ScheduleId == schedule.Id);
            var time = schedule.Time.FirstOrDefault(t => t.ScheduleId == schedule.Id);

            //create a new ScheduleViewModel formed from the schedule pulled in (above) from the database
            var scheduleViewModel = new ScheduleViewModel
            {
                CourseCode = course.CourseCode,
                CourseNumber = course.CourseNumber,
                CourseName = course.CourseName,
                DayOfTheWeek1 = dayOfTheWeek.DayOfTheWeek1,
                StartTime = time.StartTime,
                StopTime = time.StopTime,
                ScheduleId = schedule.Id
            };
              
            
            //pass ScheduleViewModel to the view
            return View(scheduleViewModel);
        }

        [HttpPost, ValidateAntiForgeryToken, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            //pull in schedule from the database (from passed in id parameter), and all it's associated data from the Course, DayOfTheWeek, and Time tables
            var schedule = _scheduleRepository.ReadSchedule(id);
            var course = schedule.Course.FirstOrDefault(s => s.ScheduleId == schedule.Id);
            var dayOfTheWeek = schedule.DayOfTheWeek.FirstOrDefault(d => d.ScheduleId == schedule.Id);
            var time = schedule.Time.FirstOrDefault(t => t.ScheduleId == schedule.Id);

            //delete the schedule and all of it's associated data from the Course, DayOfTheWeek, and Time tables
            _courseRepository.DeleteCourse(course.Id);
            _dayWeekRepository.Delete(dayOfTheWeek.Id);
            _timeRepository.DeleteTime(time.Id);
            _scheduleRepository.DeleteSchedule(schedule.Id);

            return RedirectToAction("Index", "Schedule");
        }
    }
}
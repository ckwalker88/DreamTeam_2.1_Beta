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
        private ICourseRepository _courseRepository;
        private IDayOfTheWeekRepository _dayWeekRepository;
        private ITimeRepository _timeRepository;
        private UserManager<ApplicationUser> _userManager;
        private IScheduleRepository _scheduleRepository;

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
            var applicationUserId = _userManager.GetUserId(HttpContext.User);

            var scheduleList = _scheduleRepository.ReadAllSchedules().Where(s => s.ApplicationUserId == applicationUserId);

            ICollection<ScheduleViewModel> scheduleViewModelList = new List<ScheduleViewModel>();

            Course course = new Course();
            DayOfTheWeek dayOfTheWeek = new DayOfTheWeek();
            Time time = new Time();

            foreach(var schedule in scheduleList)
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

            return View(scheduleViewModelList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Create(string courseCode, int courseNumber, string courseName, string dayOfTheWeek1, string startTime, string stopTime)
        {
            var applicationUserId = _userManager.GetUserId(HttpContext.User);
            var schedule = new Schedule { ApplicationUserId = applicationUserId };
            _scheduleRepository.CreateSchedule(schedule);

            _courseRepository.CreateCourse(new Course { CourseCode = courseCode, CourseNumber = courseNumber, CourseName = courseName, ScheduleId = schedule.Id});
            _dayWeekRepository.Create(new DayOfTheWeek { DayOfTheWeek1 = dayOfTheWeek1, ScheduleId = schedule.Id });
            _timeRepository.CreateTime(new Time { StartTime = startTime, StopTime = stopTime, ScheduleId = schedule.Id});

            return RedirectToAction("Index", "Schedule");
        }

        public IActionResult Edit(int id)
        {
            var schedule = _scheduleRepository.ReadSchedule(id);
            var course = schedule.Course.FirstOrDefault(s => s.ScheduleId == schedule.Id);
            var dayOfTheWeek = schedule.DayOfTheWeek.FirstOrDefault(d => d.ScheduleId == schedule.Id);
            var time = schedule.Time.FirstOrDefault(t => t.ScheduleId == schedule.Id);

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

            return View(editScheduleViewModel);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Edit(string courseCode, int courseNumber, string courseName, string dayOfTheWeek1, string startTime, string stopTime, int scheduleId,
                                   int courseId, int dayOfTheWeekId, int timeId)
        {
            Course course = new Course
            {
                Id = courseId,
                CourseCode = courseCode,
                CourseNumber = courseNumber,
                CourseName = courseName,
                ScheduleId = scheduleId
            };

            DayOfTheWeek dayOfTheWeek = new DayOfTheWeek
            {
                Id = dayOfTheWeekId,
                DayOfTheWeek1 = dayOfTheWeek1,
                ScheduleId = scheduleId
            };

            Time time = new Time
            {
                Id = timeId,
                StartTime = startTime,
                StopTime = stopTime,
                ScheduleId = scheduleId
            };

            _courseRepository.UpdateCourse(course);
            _dayWeekRepository.Update(dayOfTheWeek);
            _timeRepository.UpdateTime(time);

            return RedirectToAction("Index", "Schedule");
        }

        public IActionResult Delete(int id)
        {
            var schedule = _scheduleRepository.ReadSchedule(id);
            var course = schedule.Course.FirstOrDefault(s => s.ScheduleId == schedule.Id);
            var dayOfTheWeek = schedule.DayOfTheWeek.FirstOrDefault(d => d.ScheduleId == schedule.Id);
            var time = schedule.Time.FirstOrDefault(t => t.ScheduleId == schedule.Id);

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
              
            

            return View(scheduleViewModel);
        }

        [HttpPost, ValidateAntiForgeryToken, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var schedule = _scheduleRepository.ReadSchedule(id);
            var course = schedule.Course.FirstOrDefault(s => s.ScheduleId == schedule.Id);
            var dayOfTheWeek = schedule.DayOfTheWeek.FirstOrDefault(d => d.ScheduleId == schedule.Id);
            var time = schedule.Time.FirstOrDefault(t => t.ScheduleId == schedule.Id);

            _courseRepository.DeleteCourse(course.Id);
            _dayWeekRepository.Delete(dayOfTheWeek.Id);
            _timeRepository.DeleteTime(time.Id);
            _scheduleRepository.DeleteSchedule(schedule.Id);

            return RedirectToAction("Index", "Schedule");
        }
    }
}
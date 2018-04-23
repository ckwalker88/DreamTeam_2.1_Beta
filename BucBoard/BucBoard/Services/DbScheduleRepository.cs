using BucBoard.Models;
using BucBoard.Models.Entities.Existing;
using BucBoard.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BucBoard.Services
{
    public class DbScheduleRepository : IScheduleRepository
    {
        private BucBoardDBContext _db;
        private UserManager<ApplicationUser> _userManager;

        public DbScheduleRepository(BucBoardDBContext db, UserManager<ApplicationUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }

        public Schedule CreateSchedule(Schedule schedule)
        {
            _db.Schedule.Add(schedule);
            _db.SaveChanges();
            return schedule;
        }

        public Schedule ReadSchedule(int id)
        {
            return _db.Schedule
                //include the associated data from the Course, DayOfTheWeek, and Time tables
                .Include(s => s.Course)
                .Include(s => s.DayOfTheWeek)
                .Include(s => s.Time)
                .FirstOrDefault(a => a.Id == id);
        }

        public ICollection<Schedule> ReadAllSchedules()
        {
            return _db.Schedule
                //include the associated data from the Course, DayOfTheWeek, and Time tables
                .Include(s => s.Course)
                .Include(s => s.DayOfTheWeek)
                .Include(s => s.Time)
                .ToList();
        }

        public void UpdateSchedule(Schedule schedule)
        {
            _db.Entry(schedule).State = EntityState.Modified;
            _db.SaveChanges();
        }

        public void DeleteSchedule(int id)
        {
            Schedule schedule = _db.Schedule.Find(id);
            _db.Schedule.Remove(schedule);
            _db.SaveChanges();
        }
    }
}

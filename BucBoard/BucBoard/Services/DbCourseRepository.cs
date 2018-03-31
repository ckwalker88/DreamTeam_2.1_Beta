using BucBoard.Data;
using BucBoard.Models;
using BucBoard.Models.Entities.Existing;
using BucBoard.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BucBoard.Services
{
    public class DbCourseRepository : ICourseRepository
    {
        private BucBoardDBContext _db;

        public DbCourseRepository(BucBoardDBContext db)
        {
            _db = db;
        }

        public Course CreateCourse(Course course)
        {
            _db.Course.Add(course);
            _db.SaveChanges();
            return course;
        }

        public Course ReadCourse(int id)
        {
            return _db.Course.FirstOrDefault(a => a.Id == id);
        }

        public ICollection<Course> ReadAllCourses()
        {
            return _db.Course
                .Include(u => u.ApplicationUser)
                .Include(r => r.ApplicationUser.Roles)
                .ToList();
        }
        public void UpdateCourse(int id, Course course)
        {
            _db.Entry(course).State = EntityState.Modified;
            _db.SaveChanges();
        }

        public void DeleteCourse(int id)
        {
            Course course = _db.Course.Find(id);
            _db.Course.Remove(course);
            _db.SaveChanges();
        }
    }
}

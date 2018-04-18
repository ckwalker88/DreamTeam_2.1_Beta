using BucBoard.Models.Entities.Existing;
using System.Collections.Generic;

namespace BucBoard.Services.Interfaces
{
    public interface ICourseRepository
    {
        Course CreateCourse(Course course);
        Course ReadCourse(int id);
        ICollection<Course> ReadAllCourses();
        void UpdateCourse(int id, Course course);
        void DeleteCourse(int id);
    }
}

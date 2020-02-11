using CoursesScheduler.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoursesScheduler.Repositories
{
    public interface ICourseRepository
    {
        IEnumerable<Course> GetAll();
        Course GetById(int id);
        void Create(Course item);
        void Update(Course item);
        void Remove(Course id);
        bool HasIntersectingCourses(Course item);
    }
}

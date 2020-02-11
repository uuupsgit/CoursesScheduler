using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoursesScheduler.Data;
using CoursesScheduler.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace CoursesScheduler.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        CourseContext courseContext;
        public CourseRepository(CourseContext courseContext)
        {
            this.courseContext = courseContext;
        }

        public void Create(Course course)
        {
            courseContext.Add(course);
            courseContext.SaveChanges();
        }

        public IEnumerable<Course> GetAll()
        {
            return courseContext.Courses.ToList();
        }

        public Course GetById(int id)
        {
            return courseContext.Courses.FirstOrDefault(x => x.Id == id);
        }

        public void Update(Course item)
        {
            courseContext.Entry(item).State = EntityState.Modified;
            courseContext.SaveChanges();
        }

        public void Remove(Course item)
        {
            courseContext.Remove(item);
            courseContext.SaveChanges();
        }

        public bool HasIntersectingCourses(Course item)
        {
            return courseContext.Courses.Any(x => x.Id != item.Id && x.DayOfWeek == item.DayOfWeek && ((x.TimeStart > item.TimeStart && x.TimeStart < item.TimeEnd) || (x.TimeEnd > item.TimeStart && x.TimeEnd < item.TimeEnd) || (x.TimeEnd == item.TimeEnd && x.TimeStart == item.TimeStart)));
        }
    }
}

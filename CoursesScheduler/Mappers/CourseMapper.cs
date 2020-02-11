using CoursesScheduler.Data.Models;
using CoursesScheduler.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoursesScheduler.Mappers
{
    public class CourseMapper
    {
        public static CourseViewModel MapToCourseViewModel(Course item)
        {
            CourseViewModel courseView = new CourseViewModel();
            courseView.Id = item.Id;
            courseView.Name = item.Name;
            courseView.Price = item.Price;
            courseView.DayOfWeek = item.DayOfWeek;
            courseView.TimeStart = item.TimeStart.ToString(@"hh\:mm");
            courseView.TimeEnd = item.TimeEnd.ToString(@"hh\:mm");
            return courseView;
        }
    }
}

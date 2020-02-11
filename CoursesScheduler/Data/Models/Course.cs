using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CoursesScheduler.Data.Models
{
    public class Course
    {
        public int Id { get; set; }
        [StringLength(256)]
        public string Name { get; set; }
        public double Price { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        public TimeSpan TimeStart { get; set; }
        public TimeSpan TimeEnd { get; set; }
    }
}

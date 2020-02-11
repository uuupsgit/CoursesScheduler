using CoursesScheduler.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoursesScheduler.Data
{
    public class CourseContext: DbContext
    {
        public CourseContext(DbContextOptions<CourseContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Course>().HasData(
            new Course[]
            {
                new Course { Id=1, Name="Python", Price=100, DayOfWeek = DayOfWeek.Monday, TimeStart = new TimeSpan(10, 0, 0), TimeEnd = new TimeSpan(11, 0, 0) },
                new Course { Id=2, Name=".Net", Price=2250, DayOfWeek = DayOfWeek.Thursday, TimeStart = new TimeSpan(9, 0, 0), TimeEnd = new TimeSpan(10, 0, 0) },
                new Course { Id=3, Name="Js", Price=188.20, DayOfWeek = DayOfWeek.Friday, TimeStart = new TimeSpan(10, 0, 0), TimeEnd = new TimeSpan(12, 0, 0) }
            });
        }

        public DbSet<Course> Courses { get; set; }
    }
}

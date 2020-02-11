using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using CoursesScheduler.Data;
using CoursesScheduler.Data.Models;
using CoursesScheduler.Mappers;
using CoursesScheduler.Repositories;
using CoursesScheduler.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;

namespace CoursesScheduler.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        ICourseRepository courseRepository;

        public CoursesController(ICourseRepository courseRepository)
        {
            this.courseRepository = courseRepository;
        }
        
        [HttpGet]
        public ActionResult<IEnumerable<CourseViewModel>> Get()
        {
            var data = courseRepository.GetAll();
            return data.Select(x => CourseMapper.MapToCourseViewModel(x)).ToList();
        }

        /// <response code="404">If the course was not found</response>  
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<CourseViewModel> Get(int id)
        {
            var course = courseRepository.GetById(id);
            if (course == null)
                return NotFound();
            return CourseMapper.MapToCourseViewModel(course);
        }

        /// <returns>A newly created course</returns>
        /// <response code="400">If the model is not valid</response>   
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<CourseViewModel> Post([FromBody] CourseCreationViewModel model)
        {
            CultureInfo culture = CultureInfo.CurrentCulture;
            TimeSpan timeStart, timeEnd;

            if (!TimeSpan.TryParseExact(model.TimeStart, @"hh\:mm", culture, out timeStart))
                ModelState.AddModelError("TimeStart", "Недопустимое время начала курса");

            if(!TimeSpan.TryParseExact(model.TimeEnd, @"hh\:mm", culture, out timeEnd))
                ModelState.AddModelError("TimeEnd", "Недопустимое время окончания курса");

            if (timeStart >= timeEnd)
                ModelState.AddModelError("TimeStart", "Время начала курса не может быть больше или равно времени окончания курса");
            if (timeStart < new TimeSpan(9, 0, 0) || timeStart > new TimeSpan(17, 0, 0))
                ModelState.AddModelError("TimeStart", "Время курса должно быть в диапазоне 09:00-17:00");
            if (timeEnd < new TimeSpan(9, 0, 0) || timeEnd > new TimeSpan(17, 0, 0))
                ModelState.AddModelError("TimeEnd", "Время курса должно быть в диапазоне 09:00-17:00");
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
          
            var course = new Course();
            course.Name = model.Name;
            course.TimeEnd = timeEnd;
            course.TimeStart = timeStart;
            course.Price = model.Price;
            course.DayOfWeek = model.DayOfWeek;
            if (courseRepository.HasIntersectingCourses(course))
            {
                ModelState.AddModelError("TimeStart_TimeEnd", "Время проведения курса не должно пересекаться с другими курсами");
                return BadRequest(ModelState);
            }
            courseRepository.Create(course);
            return Ok(CourseMapper.MapToCourseViewModel(course));
        }

        /// <returns>A newly edited course</returns>
        /// <response code="400">If the model is not valid</response>  
        /// <response code="404">If the course was not found</response>  
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<CourseViewModel> Put(int id, [FromBody] CourseCreationViewModel model)
        {
            CultureInfo culture = CultureInfo.CurrentCulture;
            TimeSpan timeStart, timeEnd;
            if (!TimeSpan.TryParseExact(model.TimeStart, @"hh\:mm", culture, out timeStart))
                ModelState.AddModelError("TimeStart", "Недопустимое время начала курса");

            if (!TimeSpan.TryParseExact(model.TimeEnd, @"hh\:mm", culture, out timeEnd))
                ModelState.AddModelError("TimeEnd", "Недопустимое время окончания курса");

            if (timeStart >= timeEnd)
                ModelState.AddModelError("TimeStart", "Время начала курса не может быть больше или равно времени окончания курса");
            if (timeStart < new TimeSpan(9, 0, 0) || timeStart > new TimeSpan(17, 0, 0))
                ModelState.AddModelError("TimeStart", "Время курса должно быть в диапазоне 09:00-17:00");
            if (timeEnd < new TimeSpan(9, 0, 0) || timeEnd > new TimeSpan(17, 0, 0))
                ModelState.AddModelError("TimeEnd", "Время курса должно быть в диапазоне 09:00-17:00");
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            var course = courseRepository.GetById(id);
            if (course == null)
                return NotFound();
            course.Name = model.Name;
            course.TimeEnd = timeEnd;
            course.TimeStart = timeStart;
            course.Price = model.Price;
            course.DayOfWeek = model.DayOfWeek;
            if (courseRepository.HasIntersectingCourses(course))
            {
                ModelState.AddModelError("TimeStart_TimeEnd", "Время проведения курса не должно пересекаться с другими курсами");
                return BadRequest(ModelState);
            }
            courseRepository.Update(course);
            return CourseMapper.MapToCourseViewModel(course);
        }

        /// <response code="404">If the course was not found</response>   
        /// <response code="204">Course was successfully deleted</response>  
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult Delete(int id)
        {
            var course = courseRepository.GetById(id);
            if (course == null)
                return NotFound();
            courseRepository.Remove(course);
            return NoContent();
        }
    }
}

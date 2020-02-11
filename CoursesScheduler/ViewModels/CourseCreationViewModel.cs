using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CoursesScheduler.ViewModels
{
    public class CourseCreationViewModel
    {
        [Required(ErrorMessage = "Не указано название курса")]
        [StringLength(256, ErrorMessage = "Название курса не может превышать 256 символов")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Не указана стоимость курса")]
        public double Price { get; set; }
        [Required(ErrorMessage = "Не указан день проведения курса")]
        [Range(1, 5, ErrorMessage = "Недопустимый день недели")]
        public DayOfWeek DayOfWeek { get; set; }
        [Required(ErrorMessage = "Не указано время начала курса")]
        [RegularExpression(@"[0-1]\d:[0-5]\d", ErrorMessage = "Некорректное время начала курса")]
        public string TimeStart { get; set; }
        [Required(ErrorMessage = "Не указано время окончания курса")]
        [RegularExpression(@"[0-1]\d:[0-5]\d", ErrorMessage = "Некорректное время окончания курса")]
        public string TimeEnd { get; set; }
    }
}

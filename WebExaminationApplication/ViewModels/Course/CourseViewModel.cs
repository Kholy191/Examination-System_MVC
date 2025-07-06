using System.ComponentModel.DataAnnotations;

namespace WebExaminationApplication.ViewModels.Course
{
    public class CourseViewModel
    {
        [Required]
        [MaxLength(50)]
        [MinLength(5)]
        public string Title { get; set; }
        [Required]
        [MaxLength(200)]
        [MinLength(10)]
        public string Description { get; set; }
        public int Credits { get; set; }
    }
}
using Domain.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebExaminationApplication.ViewModels.Course
{
    public class CourseEditViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Credits { get; set; }
        public string? InstructorName { get; set; } = string.Empty;
        public int? InstructorId { get; set; } //InstructorId
        public List<SelectListItem> Instructors { get; set; } = new();
    }
}

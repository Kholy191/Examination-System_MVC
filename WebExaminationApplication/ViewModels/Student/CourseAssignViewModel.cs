using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebExaminationApplication.ViewModels.Student
{
    public class CourseAssignViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CourseId { get; set; }
        public List<SelectListItem> Courses { get; set; } = new();
    }
}

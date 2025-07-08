using Shared.Dtos.Exam;
using Shared.Dtos.Student;

namespace WebExaminationApplication.Controllers.Course
{
    public class CourseWithExamDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Credits { get; set; }
        public IEnumerable<ExamIndexDto> Exams { get; set; }
    }
}

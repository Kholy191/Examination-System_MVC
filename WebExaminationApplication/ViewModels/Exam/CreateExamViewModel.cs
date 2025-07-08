namespace WebExaminationApplication.ViewModels.Exam
{
    public class CreateExamViewModel
    {
        public int? CourseId { get; set; }
        public string Title { get; set; }
        public DateTime ExamDate { get; set; }
        public int DurationInMinutes { get; set; }
    }
}

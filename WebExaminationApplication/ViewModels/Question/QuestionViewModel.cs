using WebExaminationApplication.ViewModels.Answer;

namespace WebExaminationApplication.ViewModels.Question
{
    public class QuestionViewModel
    {
        public int? Id { get; set; }
        public string Text { get; set; }
        public int Marks { get; set; }
        public int ExamId { get; set; }
        public List<AnswerViewModel> Answers { get; set; }
        
    }
}

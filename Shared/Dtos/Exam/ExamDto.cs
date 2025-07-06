using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Dtos.Question;

namespace Shared.Dtos.Exam
{
    public class ExamDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int TotalQuestions { get; set; }
        public int TotalMarks { get; set; }
        public IEnumerable<QuestionDto> questionDtos { get; set; }
    }
}

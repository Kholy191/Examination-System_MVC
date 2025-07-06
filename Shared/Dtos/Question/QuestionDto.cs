using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Dtos.Answer;

namespace Shared.Dtos.Question
{
    public class QuestionDto
    {
        public int? Id { get; set; }
        public string Text { get; set; }
        public string Answer { get; set; }
        public int Marks { get; set; }
        public int ExamId { get; set; }
        public IEnumerable<AnswerDto> Answers { get; set; }
    }
}

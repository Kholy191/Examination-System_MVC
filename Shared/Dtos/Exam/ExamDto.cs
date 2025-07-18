﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Dtos.Question;

namespace Shared.Dtos.Exam
{
    public class ExamDto
    {
        public int? Id { get; set; }
        public string Title { get; set; }
        public DateTime ExamDate { get; set; }
        public int DurationInMinutes { get; set; }
        public int CourseId { get; set; }
        public IEnumerable<QuestionDto> questionDtos { get; set; }
    }
}

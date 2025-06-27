using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Question : BaseEntity<int>
    {
        public string Text { get; set; }
        public string Answer { get; set; }
        public List<string> Options { get; set; } = new List<string>();
        public int Marks { get; set; }

        #region Exam Relationship
        public int ExamId { get; set; } // Foreign key for Exam
        public Exam Exam { get; set; } // Assuming a question belongs to one exam
        #endregion
    }
}

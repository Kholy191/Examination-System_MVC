using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class StudentExam
    {
        public int StudentId { get; set; } // Foreign key for Student
        public int ExamId { get; set; } // Foreign key for Exam

        #region Navigation Properties
        public Student Student { get; set; } // Navigation property for Student
        public Exam Exam { get; set; } // Navigation property for Exam
        #endregion

        public int MarksObtained { get; set; } // Marks obtained by the student in the exam
    }
}

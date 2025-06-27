using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Exam : BaseEntity<int>
    {
        public string Title { get; set; }
        public DateTime ExamDate { get; set; }
        public int DurationInMinutes { get; set; }

        #region Course Relationship
        [ForeignKey("Course")]
        public int CourseId { get; set; } // Foreign key for Course
        public Course Course { get; set; } // Assuming an exam belongs to one course
        #endregion

        #region Question Relationship
        public ICollection<Question> Questions { get; set; } = new HashSet<Question>();
        #endregion

        #region Student Relationship
        public ICollection<StudentExam> StudentExams { get; set; } = new HashSet<StudentExam>();
        #endregion
    }
}

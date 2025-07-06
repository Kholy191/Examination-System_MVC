using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Course : BaseEntity<int>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int Credits { get; set; }

        #region Instructor Relationship
        [ForeignKey("Instructor")]
        public int? InstructorId { get; set; } // Foreign key for Instructor
        public Instructor Instructor { get; set; } // Assuming a course has one instructor
        #endregion

        #region Student Relationship
        public ICollection<Student> Students { get; set; } = new HashSet<Student>();
        #endregion

        #region Exam Relationship
        public ICollection<Exam> Exams { get; set; } = new HashSet<Exam>();
        #endregion

    }
}

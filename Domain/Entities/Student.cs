using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Student : BaseEntity<int> 
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        #region Course Relationship
        public ICollection<Course> Courses { get; set; } = new HashSet<Course>();
        #endregion

        #region Exam Relationship
        public ICollection<StudentExam> StudentExams { get; set; } = new HashSet<StudentExam>();
        #endregion

        #region User RelationShip
        [ForeignKey("ApplicationUser")]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        #endregion
    }
}

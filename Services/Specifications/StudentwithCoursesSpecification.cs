using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Services.Specification_Implementation
{
    internal class StudentwithCoursesSpecification : Specifications.Specifications<Student, int>
    {
        public StudentwithCoursesSpecification(int id) :
            base(c => c.Id == id)
        {
            AddInclude(c => c.Courses);
        }
    }
}

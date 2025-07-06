using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Services.Specifications
{
    internal class CourseByInstructorSpecification : Specifications<Course, int>
    {
        public CourseByInstructorSpecification(int id) :
        base(c => c.InstructorId == id)
        {
        }
    }
}

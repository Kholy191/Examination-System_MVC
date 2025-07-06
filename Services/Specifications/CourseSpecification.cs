using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Services.Specification_Implementation
{
    internal class CoursewithInstructorSpecification : Specifications.Specifications<Course , int>
    {
        public CoursewithInstructorSpecification(int id) : 
            base(c => c.Id == id)
        {
            AddInclude(c => c.Instructor);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Services.Specifications
{
    internal class CoursewithStudentSpecification : Specifications<Course ,int>
    {
        public CoursewithStudentSpecification(int id) :
        base(c => c.Id == id)
        {
            AddInclude(c => c.Students);
        }
    }
}

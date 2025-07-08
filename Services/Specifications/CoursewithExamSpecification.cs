using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Services.Specifications
{
    public class CoursewithExamSpecification : Specifications<Course,int>
    {
        public CoursewithExamSpecification(int id) :
        base(c => c.Id == id)
        {
            AddInclude(c => c.Exams);
        }
    }
}

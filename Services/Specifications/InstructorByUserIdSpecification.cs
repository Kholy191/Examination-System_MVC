using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Services.Specifications
{
    public class InstructorByUserIdSpecification : Specifications<Instructor,int>
    {
        public InstructorByUserIdSpecification(string id) :
           base(c => c.UserId.Equals(id))
        {
        }
    }
}

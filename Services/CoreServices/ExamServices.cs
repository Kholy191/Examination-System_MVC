using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Contracts;
using ServicesAbstraction.CoreServices;
using Shared.Dtos.Exam;

namespace Services.CoreServices
{
    public class ExamServices(IUnitofWork unitofWork) : IExamServices
    {
        public Task CreateAsync(ExamDto examDto)
        {
            throw new NotImplementedException();
        }
    }
}

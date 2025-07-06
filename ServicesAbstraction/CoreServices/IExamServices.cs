using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Dtos.Exam;

namespace ServicesAbstraction.CoreServices
{
    public interface IExamServices
    {
        public Task CreateAsync(ExamDto examDto);
    }
}

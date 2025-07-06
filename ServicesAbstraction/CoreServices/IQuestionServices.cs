using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Dtos.Question;

namespace ServicesAbstraction.CoreServices
{
    public interface IQuestionServices
    {
        public Task CreateAsync(QuestionDto questionDto);
    }
}

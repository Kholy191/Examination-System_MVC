using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Dtos.Answer;

namespace ServicesAbstraction.CoreServices
{
    public interface IAnswerOptionService
    {
        public Task CreateAsync(AnswerDto answerOption);
    }
}

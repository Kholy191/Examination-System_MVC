using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.Entities;
using ServicesAbstraction.CoreServices;
using Shared.Dtos.Answer;

namespace Services.CoreServices
{
    public class AnswerOptionService(IUnitofWork unitofWork) : IAnswerOptionService
    {
        public async Task CreateAsync(AnswerDto answerOption)
        {
            var Repo = unitofWork.GetRepository<AnswerOption, int>();
            await Repo.AddAsync(new AnswerOption()
            {
                QuestionId = answerOption.QuestionId,
                Text = answerOption.Text,
                IsCorrect = answerOption.IsCorrect,
            });
            await unitofWork.SaveChangesAsync();
        }
    }
}

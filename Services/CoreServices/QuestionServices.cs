using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.Entities;
using ServicesAbstraction.CoreServices;
using Shared.Dtos.Answer;
using Shared.Dtos.Exam;
using Shared.Dtos.Question;

namespace Services.CoreServices
{
    public class QuestionServices(IUnitofWork unitofWork, IAnswerOptionService answerOptionServices) : IQuestionServices
    {
        public async Task CreateAsync(QuestionDto questionDto)
        {
            var Repo = unitofWork.GetRepository<Question, int>();
            var question = new Question()
            {
                Marks = questionDto.Marks,
                Text = questionDto.Text,
                ExamId = questionDto.ExamId
            };
            await Repo.AddAsync(question);
            await unitofWork.SaveChangesAsync();
            foreach (var item in questionDto.Answers)
            {
                var AnswerOption = new AnswerDto()
                {
                    Text = item.Text,
                    QuestionId = question.Id,
                    IsCorrect = item.IsCorrect,
                };
                await answerOptionServices.CreateAsync(AnswerOption);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.Entities;
using Services.Specifications;
using ServicesAbstraction.CoreServices;
using Shared.Dtos.Exam;
using Shared.Dtos.Question;

namespace Services.CoreServices
{
    public class ExamServices(IUnitofWork unitofWork) : IExamServices
    {
        public async Task<int> CreateAsync(ExamDto examDto)
        {
            var Repo = unitofWork.GetRepository<Exam, int>();
            var exam = new Exam()
            {
                CourseId = examDto.CourseId,
                Title = examDto.Title,
                DurationInMinutes = examDto.DurationInMinutes,
                ExamDate = examDto.ExamDate
            };
            await Repo.AddAsync(exam);
            await unitofWork.SaveChangesAsync();
            return exam.Id;
        }

        public async Task<ExamDto> GetDetailsAsync(int Id)
        {
            var Repo = unitofWork.GetRepository<Exam, int>();
            var specification = new ExamwithQuestionSpecification(Id);
            var Exam = await Repo.GetBySpecificationAsync(Id, specification);
            return new ExamDto()
            {
                Id = Exam.Id,
                CourseId = Exam.CourseId,
                Title = Exam.Title,
                DurationInMinutes = Exam.DurationInMinutes,
                ExamDate = Exam.ExamDate,
                questionDtos = Exam.Questions != null
                                ? Exam.Questions.Select(x => new QuestionDto
                                {
                                    Id = x.Id,
                                    Marks = x.Marks,
                                    Text = x.Text
                                })
                                : null!
            };
        }
    }
}

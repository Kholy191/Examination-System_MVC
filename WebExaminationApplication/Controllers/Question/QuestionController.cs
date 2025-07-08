using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServicesAbstraction;
using ServicesAbstraction.CoreServices;
using Shared.Dtos.Answer;
using Shared.Dtos.Question;
using WebExaminationApplication.ViewModels;
using WebExaminationApplication.ViewModels.Question;

namespace WebExaminationApplication.Controllers.Question
{
    public class QuestionController(IManagerService services, IQuestionServices questionServices) : Controller
    {
        #region Create
        [HttpGet]
        [Authorize(Roles = "Instructor")]
        public IActionResult Create(int Id)
        {
            var QuestionModel = new QuestionViewModel() { ExamId = Id };
            return View(QuestionModel);
        }

        [HttpPost]
        [Authorize(Roles = "Instructor")]
        public async Task<IActionResult> Create(QuestionViewModel model)
        {
            if (!model.Answers.Any(x => x.IsCorrect == true))
            {
                ModelState.AddModelError("Answers", "At least one answer should be correct");
                return View(model);
            }
            var question = new QuestionDto(){
                ExamId = model.ExamId,
                Marks = model.Marks,
                Text = model.Text,
                Answers = model.Answers.Select(x => new AnswerDto()
                {
                    Text = x.Text,
                    IsCorrect = x.IsCorrect,
                }).ToList()
            };
            await questionServices.CreateAsync(question);
            return RedirectToAction("ViewExam", "Exam", new { id = model.ExamId });
        }
        #endregion
    }
}

using AspNetCoreGeneratedDocument;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServicesAbstraction;
using ServicesAbstraction.CoreServices;
using Shared.Dtos.Exam;
using WebExaminationApplication.ViewModels.Exam;

namespace WebExaminationApplication.Controllers.Exam
{
    public class ExamController(IManagerService services, IQuestionServices questionServices) : Controller
    {
        #region Create
        [HttpGet]
        [Authorize(Roles = "Instructor")]
        public IActionResult Create(int Id)
        {
            var model = new CreateExamViewModel { CourseId = Id };
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Instructor")]
        public async Task<IActionResult> Create(CreateExamViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            if(model.CourseId == null)
            {
                return BadRequest();
            }
            var examDto = new ExamDto()
            {
                CourseId = (int)model.CourseId!,
                DurationInMinutes = model.DurationInMinutes,
                ExamDate = model.ExamDate,
                Title = model.Title,
            };
            var ExamId = await services.ExamServices.CreateAsync(examDto);
            return RedirectToAction("ViewExam", ExamId);
        }
        #endregion

        [HttpGet]
        [Authorize(Roles = "Instructor")]
        public async Task<IActionResult> ViewExam(int Id)
        {
            var ExamDetails = await services.ExamServices.GetDetailsAsync(Id);
            return View(ExamDetails);
        }
    }
}

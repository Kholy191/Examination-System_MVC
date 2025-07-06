using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ServicesAbstraction;
using WebExaminationApplication.ViewModels.Student;

namespace WebExaminationApplication.Controllers.Student
{
    public class StudentController(IManagerService _service) : Controller
    {
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            var Students = await _service.StudentServices.GetAllStudentsAsync();
            return View(Students);
        }

        #region Course Assign

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AssignCourse(int Id)
        {
            var Courses = await _service.CourseServices.GetAllAsync();
            var Student = await _service.StudentServices.GetStudentByIdAsync(Id);
            if (Student == null) return NotFound();
            if (Courses == null) return NotFound();
            var ViewModel = new CourseAssignViewModel()
            {
                Name = Student.FirstName + " " + Student.LastName,
                Id = Student.Id,
                Courses = Courses.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Title
                }).ToList()
            };
            return View(ViewModel);
        }


        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AssignCourse(CourseAssignViewModel _model)
        {
            await _service.StudentServices.AssignStudentToCourseAsync(_model.Id, _model.CourseId);
            return RedirectToAction("Index");
        }

        #endregion

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> StudentDetails(int Id)
        {
            var Student = await _service.StudentServices.GetStudentDetailsDtoAsync(Id);
            if (Student == null) return NotFound();
            return View(Student);
        }
    }
}

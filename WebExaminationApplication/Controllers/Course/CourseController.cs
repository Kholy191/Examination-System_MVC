using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ServicesAbstraction;
using Shared.Dtos;
using Shared.Dtos.Course;
using WebExaminationApplication.ViewModels.Course;

namespace WebExaminationApplication.Controllers.Course
{
    public class CourseController(IManagerService services) : Controller
    {
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            var Courses = await services.CourseServices.GetAllAsync();
            if (Courses != null)
            {
                return View(Courses);
            }
            return RedirectToAction("Create");
        }

        #region Create
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(CourseViewModel _course)
        {
            if (!ModelState.IsValid)
            {
                return View(_course);
            }
            var NewCourseDto = new CourseDto()
            {
                Title = _course.Title,
                Description = _course.Description,
                Credits = _course.Credits,
            };
            await services.CourseServices.CreateCourseAsync(NewCourseDto);
            return RedirectToAction("Index");
        }
        #endregion

        #region Edit
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int Id)
        {
            var Course = await services.CourseServices.GetDetailsAsync(Id);
            var instructors = await services.InstructorServices.GetAllInstructorsAsync();
            var CourseView = new CourseEditViewModel()
            {
                Id = Course.Id,
                Title = Course.Title,
                Description = Course.Description,
                Credits = Course.Credits,
                InstructorId = Course.InstructorId,
                InstructorName = Course.Instructor,
                Instructors = instructors.Select(i => new SelectListItem
                {
                    Value = i.Id.ToString(),
                    Text = $"{i.FirstName} {i.LastName}"
                }).ToList()
            };
            if (Course != null)
            {
                return View(CourseView);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(CourseEditViewModel _course)
        {
            if (!ModelState.IsValid)
            {
                return View(_course);
            }
            var NewCourseDto = new CourseDetailsDto()
            {
                Id = _course.Id,
                Title = _course.Title,
                Description = _course.Description,
                Credits = _course.Credits,
                InstructorId = _course.InstructorId,
            };
            await services.CourseServices.UpdateCourseAsync(NewCourseDto);
            return RedirectToAction("Index");
        }
        #endregion

        #region Delete
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int Id)
        {
            await services.CourseServices.DeleteCourseAsync(Id);
            return RedirectToAction("Index");
        }
        #endregion

        #region Course and its Students
        [HttpGet]
        [Authorize(Roles = "Admin , Instructor")]
        public async Task<IActionResult> CoursewithStudents(int Id)
        {
            var CoursesStudentsDto = await services.CourseServices.GetCourseStudents(Id);
            return View(CoursesStudentsDto);
        }
        #endregion

        #region Course and its Exams
        [HttpGet]
        [Authorize(Roles = "Instructor")]
        public async Task<IActionResult> CoursewithExams(int Id)
        {
            var CoursesExamsDto = await services.CourseServices.GetCourseExams(Id);
            return View(CoursesExamsDto);
        }
        #endregion

    }
}

using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServicesAbstraction;
using ServicesAbstraction.CoreServices;

namespace WebExaminationApplication.Controllers.Instructor
{
    public class InstructorController(IManagerService service, IAuthenticationService authenticationService) : Controller
    {
        [HttpGet]
        [Authorize(Roles = "Instructor")]
        public async Task<IActionResult> MyCourses()
        {
            var CurrentUser = await authenticationService.GetCurrentUserAsync(User.Identity!.Name!);
            var CurrentInstructor = await service.InstructorServices.GetInstructorByUserIdAsync(CurrentUser.UserId);
            var Courses = await service.InstructorServices.GetCoursesByInstructorIdAsync((int)CurrentInstructor.Id!);
            return View(Courses);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Dtos.Course;
using Shared.Dtos.Exam;
using WebExaminationApplication.Controllers.Course;

namespace ServicesAbstraction.CoreServices
{
    public interface ICourseServices
    {
        public Task CreateCourseAsync(CourseDto _course);
        public Task<IEnumerable<CourseDto>?> GetAllAsync();
        public Task<CourseDetailsDto> GetDetailsAsync(int id);
        public Task UpdateCourseAsync(CourseDetailsDto _course);
        public Task DeleteCourseAsync(int id);
        public Task<CoursewithStudentsDto> GetCourseStudents(int id);
        public Task<CourseWithExamDto> GetCourseExams(int id);
    }
}

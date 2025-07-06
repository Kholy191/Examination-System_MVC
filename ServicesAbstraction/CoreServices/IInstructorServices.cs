using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Dtos.Course;
using Shared.Dtos.Instructor;

namespace ServicesAbstraction.CoreServices
{
    public interface IInstructorServices
    {
        public Task AddInstructorAsync(InstructorDto _student);
        public Task UpdateInstructorAsync(InstructorDto _student);
        public Task DeleteInstructorAsync(int id);
        public Task<InstructorDto> GetInstructorByIdAsync(int id);
        public Task<IEnumerable<InstructorDto>> GetAllInstructorsAsync();
        public Task<IEnumerable<CourseDto>> GetCoursesByInstructorIdAsync(int instructorId);
        public Task<InstructorDto> GetInstructorByUserIdAsync(string userId);

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Dtos.Student;

namespace ServicesAbstraction.CoreServices
{
    public interface IStudentServices
    {
        public Task AddStudentAsync(StudentDto _student);
        public Task DeleteStudentAsync(int id);
        public Task<IEnumerable<StudentDto>> GetAllStudentsAsync();
        public Task<StudentDto> GetStudentByIdAsync(int id);
        public Task UpdateStudentAsync(int id, StudentDto _student);
        public Task<StudentDetailsDto> GetStudentDetailsDtoAsync(int id);
        public Task AssignStudentToCourseAsync(int studentId, int courseId);
    }
}

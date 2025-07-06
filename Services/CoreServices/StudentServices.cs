using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.Entities;
using Services.Specification_Implementation;
using ServicesAbstraction.CoreServices;
using Shared.Dtos.Course;
using Shared.Dtos.Student;

namespace Services.CoreServices
{
    public class StudentServices : IStudentServices
    {
        readonly IUnitofWork unitofWork;
        public StudentServices(IUnitofWork _unitofwork)
        {
            unitofWork = _unitofwork;
        }
        #region Old Methods
        public async Task AddStudentAsync(StudentDto _student)
        {
            var Repo = unitofWork.GetRepository<Student, int>();
            var NewStudent = new Student()
            {
                Email = _student.Email,
                FirstName = _student.FirstName,
                LastName = _student.LastName,
                PhoneNumber = _student.PhoneNumber,
                UserId = _student.UserId!
            };
            await Repo.AddAsync(NewStudent);
            await unitofWork.SaveChangesAsync();
        }

        public Task DeleteStudentAsync(int id)
        {
            var Repo = unitofWork.GetRepository<Student, int>();
            Repo.Delete(id);
            return unitofWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<StudentDto>> GetAllStudentsAsync()
        {
            var Repo = unitofWork.GetRepository<Student, int>();
            var Students = await Repo.GetAllAsync();
            var DtosResult = Students.Select(x => new StudentDto()
            {
                Id = x.Id,
                Email = x.Email,
                FirstName = x.FirstName,
                LastName = x.LastName,
                PhoneNumber = x.PhoneNumber,
            });
            return DtosResult;
        }

        public async Task<StudentDto> GetStudentByIdAsync(int id)
        {
            var Repo = unitofWork.GetRepository<Student, int>();
            var Student = await Repo.GetByIdAsync(id);
            return new StudentDto()
            {
                Id = Student.Id,
                Email = Student.Email,
                FirstName = Student.FirstName,
                LastName = Student.LastName,
                PhoneNumber = Student.PhoneNumber,
            };
        }

        public Task UpdateStudentAsync(int id, StudentDto _student)
        {
            throw new NotImplementedException();
        }

        public async Task AssignStudentToCourseAsync(int studentId, int courseId)
        {
            var Repo = unitofWork.GetRepository<Student, int>();
            var course = await unitofWork.GetRepository<Course, int>().GetByIdAsync(courseId);
            var student = await Repo.GetByIdAsync(studentId);
            var studentwithDetails = await GetStudentDetailsDtoAsync(studentId);
            if(studentwithDetails.Courses?.Any(x => x == course.Title ) == false)
            {
                student.Courses.Add(course); 
            }
            await unitofWork.SaveChangesAsync();
        }
        #endregion
        public async Task<StudentDetailsDto> GetStudentDetailsDtoAsync(int id)
        {
            var Repo = unitofWork.GetRepository<Student, int>();
            var specification = new StudentwithCoursesSpecification(id);
            var student = await Repo.GetBySpecificationAsync(id,specification);
            var StudentDto = new StudentDetailsDto()
            {
                Id = student.Id,
                Email = student.Email,
                FirstName = student.FirstName,
                LastName = student.LastName,
                PhoneNumber = student.PhoneNumber,
                Courses = student.Courses?.Select(x => x.Title)!,
            };
            return StudentDto;
        }
    }
}

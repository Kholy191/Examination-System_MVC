using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.Entities;
using Services.Specification_Implementation;
using Services.Specifications;
using ServicesAbstraction.CoreServices;
using Shared.Dtos.Course;
using Shared.Dtos.Student;

namespace Services.CoreServices
{
    public class CourseServices(IUnitofWork _unitofWork) : ICourseServices
    {
        public async Task CreateCourseAsync(CourseDto _course)
        {
            var Repo = _unitofWork.GetRepository<Course, int>();
            var NewCourse = new Course()
            {
                Title = _course.Title,
                Credits = _course.Credits,
                Description = _course.Description
            };
            await Repo.AddAsync(NewCourse);
            await _unitofWork.SaveChangesAsync();
        }

        public async Task DeleteCourseAsync(int id)
        {
            var Repo = _unitofWork.GetRepository<Course, int>();
            Repo.Delete(id);
            await _unitofWork.SaveChangesAsync();
        }
        public async Task<IEnumerable<CourseDto>?> GetAllAsync()
        {
            var Repo = _unitofWork.GetRepository<Course, int>();
            var Courses = await Repo.GetAllAsync();
            if (Courses != null)
            {
                var result = Courses.Select(course => new CourseDto
                {
                    Id = course.Id,
                    Title = course.Title,
                    Description = course.Description,
                    Credits = course.Credits
                });
                return result;
            }
            return null;
        }
        public async Task<CourseDetailsDto> GetDetailsAsync(int id)
        {
            var Repo = _unitofWork.GetRepository<Course, int>();
            var specification = new CoursewithInstructorSpecification(id);
            var Course = await Repo.GetBySpecificationAsync(id,specification);
            return new CourseDetailsDto()
            {
                Id = Course.Id,
                Title = Course.Title,
                Description = Course.Description,
                Credits = Course.Credits,
                Instructor = Course.Instructor != null ? Course.Instructor.FirstName : null!,
                InstructorId = Course.Instructor?.Id
            };
        }
        public async Task UpdateCourseAsync(CourseDetailsDto _course)
        {
            var Repo = _unitofWork.GetRepository<Course, int>();
            Repo.Update(new Course()
            {
                Id = _course.Id,
                Title = _course.Title,
                Description = _course.Description,
                Credits = _course.Credits,
                InstructorId = _course.InstructorId
            });
            await _unitofWork.SaveChangesAsync();
        }
        public async Task<CoursewithStudentsDto> GetCourseStudents(int id)
        {
            var Repo = _unitofWork.GetRepository<Course, int>();
            var specification = new CoursewithStudentSpecification(id);
            var Course = await Repo.GetBySpecificationAsync(id, specification);
            var CoursewithStudents = new CoursewithStudentsDto()
            {
                Id = Course.Id,
                Title = Course.Title,
                Description = Course.Description,
                Credits = Course.Credits,
                Students = Course.Students?.Select(x => new StudentDto
                {
                    Id = x.Id,
                    Email = x.Email,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    PhoneNumber = x.PhoneNumber
                }) ?? new List<StudentDto>(),
            };
            CoursewithStudents.StudentCount = CoursewithStudents.Students.Count();
            return CoursewithStudents;
        }

    }
}

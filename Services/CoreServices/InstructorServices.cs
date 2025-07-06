using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.Entities;
using Services.Specifications;
using ServicesAbstraction.CoreServices;
using Shared.Dtos.Course;
using Shared.Dtos.Instructor;

namespace Services.CoreServices
{
    public class InstructorServices : IInstructorServices
    {
        readonly IUnitofWork unitofWork;
        public InstructorServices(IUnitofWork _unitofwork)
        {
            unitofWork = _unitofwork;
        }
        public async Task AddInstructorAsync(InstructorDto _instructor)
        {
            var Repo = unitofWork.GetRepository<Instructor, int>();
            var NewInstructor = new Instructor()
            {
                Email = _instructor.Email,
                FirstName = _instructor.FirstName,
                LastName = _instructor.LastName,
                UserId = _instructor.UserId,
                PhoneNumber = _instructor.PhoneNumber,
            };
            await Repo.AddAsync(NewInstructor);
            await unitofWork.SaveChangesAsync();
        }

        public Task DeleteInstructorAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<InstructorDto>> GetAllInstructorsAsync()
        {
            var Repo = unitofWork.GetRepository<Instructor, int>();
            var Instructors = await Repo.GetAllAsync();
            return Instructors.Select(x => new InstructorDto()
            {
                Id = x.Id,
                Email = x.Email,
                FirstName = x.FirstName,
                LastName = x.LastName,
                UserId = x.UserId,
                PhoneNumber = x.PhoneNumber,
            });
        }

        public async Task<IEnumerable<CourseDto>> GetCoursesByInstructorIdAsync(int instructorId)
        {
            var Repo = unitofWork.GetRepository<Course, int>();
            var specification = new CourseByInstructorSpecification(instructorId);
            var Courses = await Repo.GetAllBySpecificationAsync(specification);
            var CoursesDtos = Courses.Select(x => new CourseDto() { Id = x.Id, Title = x.Title, Credits = x.Credits, Description = x.Description });
            return CoursesDtos;
        }

        public Task<InstructorDto> GetInstructorByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateInstructorAsync(InstructorDto _student)
        {
            throw new NotImplementedException();
        }
        public async Task<InstructorDto> GetInstructorByUserIdAsync(string userId)
        {
            var Repo = unitofWork.GetRepository<Instructor, int>();
            var specification = new InstructorByUserIdSpecification(userId);
            var Instructor = await Repo.GetBySpecificationAsync(0, specification);
            return new InstructorDto()
            {
                Id = Instructor.Id,
                Email = Instructor.Email,
                FirstName = Instructor.FirstName,
                LastName = Instructor.LastName,
                UserId = Instructor.UserId,
                PhoneNumber = Instructor.PhoneNumber,
            };

        }

    }
}

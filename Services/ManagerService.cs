using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Services.CoreServices;
using ServicesAbstraction;
using ServicesAbstraction.CoreServices;

namespace Services
{
    public class ManagerService(IUnitofWork _unitofwork) : IManagerService
    {
        #region Services Lazy Initialization
        Lazy<ICourseServices> _courseServices = new Lazy<ICourseServices>(() => new CourseServices(_unitofwork));
        Lazy<IInstructorServices> _instructorServices = new Lazy<IInstructorServices>(() => new InstructorServices(_unitofwork));
        Lazy<IStudentExamServices> _studentExamServices = new Lazy<IStudentExamServices>(() => new StudentExamServices());
        Lazy<IExamServices> _examServices = new Lazy<IExamServices>(() => new ExamServices(_unitofwork));
        Lazy<IQuestionServices> _questionServices = new Lazy<IQuestionServices>(() => new QuestionServices(_unitofwork));
        Lazy<IStudentServices> _studentServices = new Lazy<IStudentServices>(() => new StudentServices(_unitofwork));
        #endregion

        #region Services
        public ICourseServices CourseServices => _courseServices.Value;
        public IInstructorServices InstructorServices => _instructorServices.Value;
        public IStudentExamServices StudentExamServices => _studentExamServices.Value;
        public IExamServices ExamServices => _examServices.Value;
        public IQuestionServices questionServices => _questionServices.Value;
        public IStudentServices StudentServices => _studentServices.Value;
        #endregion
    }
}
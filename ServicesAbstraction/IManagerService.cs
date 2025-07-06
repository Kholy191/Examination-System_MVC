using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServicesAbstraction.CoreServices;

namespace ServicesAbstraction
{
    public interface IManagerService
    {
        public ICourseServices CourseServices { get; }
        public IInstructorServices InstructorServices { get; }
        public IStudentExamServices StudentExamServices { get; }
        public IExamServices ExamServices { get; }
        public IQuestionServices questionServices { get; }
        public IStudentServices StudentServices { get; }
    }
}

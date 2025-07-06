using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Dtos.Student;

namespace Shared.Dtos.Course
{
    public class CoursewithStudentsDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Credits { get; set; }
        public int StudentCount { get; set; }
        public IEnumerable<StudentDto> Students { get; set; }
    }
}

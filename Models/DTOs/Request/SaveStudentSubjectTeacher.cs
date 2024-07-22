using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTOs.Request
{
    public class SaveStudentSubjectTeacher : baseRequest
    {
        public int Id { get; set; }
        public int subjectId { get; set; }
        public int teacherId { get; set; }
        public int studentId { get; set; }

    }

    public class baseRequest
    {
        public int userId { get; set; }
    }
}
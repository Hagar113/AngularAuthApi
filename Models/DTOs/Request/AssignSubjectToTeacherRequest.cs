using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTOs.Request
{
    public class AssignSubjectToTeacherRequest
    {
        public int teacherId { get; set; }
        public int subjectId { get; set; }
    }

}

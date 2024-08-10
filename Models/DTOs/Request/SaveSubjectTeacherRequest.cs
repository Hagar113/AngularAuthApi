using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTOs.Request
{
    public class SaveSubjectTeacherRequest : baseReq
    {
        public int id { get; set; }
        public int teacherId { get; set; }
        public int? subjectId { get; set; }
    }

    public class baseReq
    {
        public int userId { get; set; }
    }
}

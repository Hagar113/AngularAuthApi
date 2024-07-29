using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTOs.Request
{
    public class SaveSubjectRequest
    {
        public int? SubjectId { get; set; }
        public string SubjectName { get; set; }
        public int academicYear { get; set; }
    }

}

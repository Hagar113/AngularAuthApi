using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTOs.Request
{
    public class SaveSubjectRequest: BaseRequest
    {
        public int id { get; set; }
        public string? name { get; set; }
        public int AcademicYear { get; set; }
    }
    public class BaseRequest
    { 
        public int userId { get; set; }
    }
}

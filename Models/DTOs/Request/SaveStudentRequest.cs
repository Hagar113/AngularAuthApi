using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTOs.Request
{
    public class SaveStudentRequest
    {

        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public int userId { get; set; }
    }
}

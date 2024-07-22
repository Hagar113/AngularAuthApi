using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTOs.Request
{
    public class SaveTeacherRequest
    {

        public int id { get; set; }
        public string name { get; set; }
        public int userId { get; set; }
    }
}

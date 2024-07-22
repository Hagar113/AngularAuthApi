using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.models
{
    public class Teacher : Defaults
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [ForeignKey("Users")]
        public int? UserId { get; set; }
        public Users? User { get; set; }

        public List<TeacherSubjects> TeacherSubject { get; set; }
    }
}

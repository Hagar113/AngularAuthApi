using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.models
{
    public class Student : Defaults
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        


        [ForeignKey("User")]
        public int? UserId { get; set; }
        public Users? User { get; set; }
        // public List<StudentSubjects> StudentSubjects { get; set; }
        //public List<StudentClass> StudentClasses { get; set; }
    }
}

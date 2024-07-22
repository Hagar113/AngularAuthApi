using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.models
{
    public class StudentSubjects : Defaults
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Student")]
        public int StudentId { get; set; }
        public Student Student { get; set; }

        [ForeignKey("Subject")]
        public int SubjectId { get; set; }
        public Subjects Subject { get; set; }

        [ForeignKey("Teacher")]
        public int TeacherId { get; set; }
        public Teacher teachers { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.models
{
    public class ClassSchedule
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Class")]
        public int ClassId { get; set; }
        public Class Class { get; set; }

        [ForeignKey("DayOfWeek")]
        public int DayId { get; set; }
        public DayOfWeek DayOfWeek { get; set; }

        [ForeignKey("Subject")]
        public int SubjectId { get; set; }
        public Subjects Subject { get; set; }

        public int Hour { get; set; }
    }
}

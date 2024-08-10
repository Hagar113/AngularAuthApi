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

        [ForeignKey("Timetable")]
        public int TimetableId { get; set; }
        public Timetable Timetable { get; set; } 
    }
}

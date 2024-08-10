using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.models
{
   public class Timetable
    {
        [Key]
        public int TimetableId { get; set; }

        [ForeignKey("DayOfWeek")]
        public int DayId { get; set; }
        public DaysOfWeek DayOfWeek { get; set; }

        [ForeignKey("Subject")]
        public int? SubjectId { get; set; }
        public Subjects? Subject { get; set; }
        public int Hour { get; set; } // الوقت بالساعة (مثلاً 1 = 08:00, 2 = 09:00)

        public TimeSpan StartTime { get; set; } // الوقت الفعلي لبداية الحصة
        public TimeSpan EndTime { get; set; }   // الوقت الفعلي لنهاية الحصة 
    }
}

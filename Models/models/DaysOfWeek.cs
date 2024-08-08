using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.models
{
    public class DaysOfWeek
    {
        [Key]
        public int DayId { get; set; }
        public string DayName { get; set; }

     //   public List<ClassSchedule> ClassSchedules { get; set; }
    }
}

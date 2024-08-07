﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.models
{
    public class Subjects : Defaults
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int AcademicYear { get; set; }

        public List<ClassSchedule> ClassSchedules { get; set; }

    }
}

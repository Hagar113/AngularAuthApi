using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.models
{
    public class Roles : Defaults
    {
        [Key]
        public int id { get; set; }
        public string Name { get; set; }
        public string code { get; set; }

      


    }
}

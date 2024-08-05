using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.models
{
    public class RolePage
    {
        public int Id { get; set; }

        [ForeignKey("Roles")]
        public int RoleId { get; set; }
        public Roles Roles { get; set; }

        [ForeignKey("pages")]
        public int PageId { get; set; }
        public Pages pages { get; set; }

        
    }
}

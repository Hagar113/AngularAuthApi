using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTOs.Request
{
    public class SaveRoleReques
    {
        public int roleId { get; set; }
        public string roleName { get; set; }
        public string roleCode { get; set; }

    }
}

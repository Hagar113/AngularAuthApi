using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTOs.Request
{
    public class SavePageRoleAssociationsRequest
    {
        public int RoleId { get; set; }
        public List<int> PageIds { get; set; }
    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTOs.Request
{
    public class LoginRequest
    {
        public int userId { get; set; }
        public string? email_phone { get; set; }
        public string? password { get; set; }
        public string? languageCode { get; set; }


    }
}

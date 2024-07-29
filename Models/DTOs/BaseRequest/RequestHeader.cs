using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTOs.BaseRequest
{
    public class BaseRequestHeader
    {
        public int? userId { get; set; }
     
        public string? languageCode { get; set; }
        public object data { get; set; }
        
    }
   

}

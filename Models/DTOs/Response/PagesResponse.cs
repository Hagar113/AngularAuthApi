using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTOs.Response
{
    public class PagesResponse
    {

        public bool IsValid { get; set; }
        public List<PageDto> Pages { get; set; }
    }
    public class PageDto
    {
        public int PageId { get; set; }
        public string PageName { get; set; }
    }

}

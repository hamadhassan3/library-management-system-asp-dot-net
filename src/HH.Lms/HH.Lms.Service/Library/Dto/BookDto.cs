using HH.Lms.Service.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HH.Lms.Service.Library.Dto
{
    public class BookDto : IDto
    {
        public long Id { get; set; }
        
        public string Title { get; set; }

        public string Description { get; set; }

        public string Author { get; set; }
    }
}

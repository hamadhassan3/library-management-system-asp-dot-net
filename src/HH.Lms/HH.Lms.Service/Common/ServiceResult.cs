using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HH.Lms.Service.Common
{
    public class ServiceResult<T>
    {
        public bool Success { get; set; }
        public virtual T Data { get; set; }
        public List<string> Errors { get; set; } = new List<string>();
    }
}

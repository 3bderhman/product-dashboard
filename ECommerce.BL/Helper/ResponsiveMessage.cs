using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.BL.Helper
{
    public class ResponsiveMessage <T>
    {
        public string Code { get; set; }
        public string Status { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
    }
}

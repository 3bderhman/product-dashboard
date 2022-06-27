using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.BL.Model
{
    public class StatusVM
    {
        public enum StatusType
        {
            OnSale = 100, NotOnSale = 200
        }
        public class Status
        {
            public int Id { get; set; }
            public StatusType Value { get; set; }
        }
    }
}

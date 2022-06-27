using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.DAL.Entity
{
    public class ProductPayment
    {
        public int Id { get; set; }
        public int PaymentId { get; set; }
        public virtual Payment? Payment { get; set; }
    }
}

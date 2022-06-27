using ECommerce.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.BL.Model
{
    public class ProductPaymentVM
    {
        public int Id { get; set; }
        public int PaymentId { get; set; }
        public virtual Payment? Payment { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.BL.Interface
{
    public interface IUnitOfWork
    {
        public IProductRep ProductRep { get; set; }
        public ICategoryRep CategoryRep { get; set; }
        public IPaymentRep PaymentRep { get; set; }
    }
}

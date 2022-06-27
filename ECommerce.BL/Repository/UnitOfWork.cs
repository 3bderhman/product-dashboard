using ECommerce.BL.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.BL.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        public IProductRep ProductRep { get; set; }
        public ICategoryRep CategoryRep { get; set; }
        public IPaymentRep PaymentRep { get; set; }

        public UnitOfWork(IProductRep _ProductRep, ICategoryRep _CategoryRep, IPaymentRep _payment)
        {
            ProductRep = _ProductRep;
            CategoryRep = _CategoryRep;
            PaymentRep = _payment;
        }
    }
}

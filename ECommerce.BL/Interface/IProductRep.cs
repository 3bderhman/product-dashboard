using ECommerce.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.BL.Interface
{
    public interface IProductRep : IGenericRep<Product>
    {
        Task<IEnumerable<Product>> GetAllProductsAsync(Expression<Func<Product, bool>> filter = null);
        Task<Product> GetProductAsync(Expression<Func<Product, bool>> filter);
    }
}

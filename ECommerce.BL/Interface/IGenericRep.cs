using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.BL.Interface
{
    public interface IGenericRep<Tentity> where Tentity : class
    {
        Task<IEnumerable<Tentity>> GetAllAsync(Expression<Func<Tentity, bool>> filter = null);
        Task<Tentity> GetAsync(Expression<Func<Tentity, bool>> filter);
        Task Create (Tentity obj);
        Task Update (Tentity obj);
        Task Delete (Tentity obj);
    }
}

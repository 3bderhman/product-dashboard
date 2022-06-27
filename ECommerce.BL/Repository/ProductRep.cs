using ECommerce.BL.Interface;
using ECommerce.DAL.DataBase;
using ECommerce.DAL.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.BL.Repository
{
    public class ProductRep : GenericRep<Product>, IProductRep
    {
        private readonly ApplicationContext db;

        public ProductRep(ApplicationContext db) : base(db)
        {
            this.db = db;
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync(Expression<Func<Product, bool>> filter = null)
        {
            return filter is null ? await db.Products.Include(a => a.Tags).Include(a => a.Status).Include(a => a.Category).Include(a => a.PaymentTypes).ThenInclude(a =>a.Payment).ToListAsync() : await db.Products.Where(filter).Include(a => a.Tags).Include(a => a.Status).Include(a => a.Category).Include(a => a.PaymentTypes).ThenInclude(a => a.Payment).ToListAsync();
        }

        public async Task<Product> GetProductAsync(Expression<Func<Product, bool>> filter) => await db.Products.Where(filter).Include(a => a.Tags).Include(a => a.Status).Include(a => a.Category).Include(a => a.PaymentTypes).ThenInclude(a => a.Payment).FirstOrDefaultAsync();
        
    }
}

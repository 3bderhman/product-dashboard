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
    public class GenericRep<Tentity> : IGenericRep<Tentity> where Tentity : class
    {
        private readonly ApplicationContext db;

        public GenericRep(ApplicationContext db)
        {
            this.db = db;
        }

        public async Task<IEnumerable<Tentity>> GetAllAsync(Expression<Func<Tentity, bool>> filter = null) => filter is null ? await db.Set<Tentity>().ToListAsync() : await db.Set<Tentity>().Where(filter).ToListAsync();

        public async Task<Tentity> GetAsync(Expression<Func<Tentity, bool>> filter) => await db.Set<Tentity>().Where(filter).FirstOrDefaultAsync(); 

        public async Task Create(Tentity obj)
        {
            await db.Set<Tentity>().AddAsync(obj);
            await db.SaveChangesAsync();
        }

        public async Task Update(Tentity obj)
        {
            db.Set<Tentity>().Update(obj);
            await db.SaveChangesAsync();
        }

        public async Task Delete(Tentity obj)
        {
            db.Set<Tentity>().Remove(obj);
            await db.SaveChangesAsync();
        }
    }
}

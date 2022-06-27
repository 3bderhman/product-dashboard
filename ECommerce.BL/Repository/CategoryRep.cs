using ECommerce.BL.Interface;
using ECommerce.DAL.DataBase;
using ECommerce.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.BL.Repository
{
    public class CategoryRep : GenericRep<Category>, ICategoryRep
    {
        public CategoryRep(ApplicationContext db) : base(db)
        {

        }
    }
}

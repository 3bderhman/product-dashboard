using ECommerce.DAL.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.BL.Model
{
    public class CategoryVM
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Category Name is required")]
        public string? Value { get; set; }

    }
}

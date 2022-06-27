using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.DAL.Entity
{
    public class Product
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is required")]
        public string? Name { get; set; }
        [Required(ErrorMessage = "Description is required")]
        public string? Description { get; set; }
        [Required(ErrorMessage = "Price is required")]
        public decimal Price { get; set; }
        [DataType(DataType.Currency)]
        public decimal Discount { get; set; }
        public  int CategoryId { get; set; }
        public Category? Category { get; set; }
        public ICollection<Tag> Tags { get; set; } = new HashSet<Tag>();
        public int StatusId { get; set; }
        public Status? Status { get; set; }
        public virtual ICollection<ProductPayment> PaymentTypes { get; set; } = new HashSet<ProductPayment>();
    }
}

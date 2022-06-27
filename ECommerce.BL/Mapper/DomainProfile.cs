using AutoMapper;
using ECommerce.BL.Model;
using ECommerce.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.BL.Mapper
{
    public class DomainProfile : Profile
    {
        public DomainProfile()
        {
            CreateMap<CategoryVM, Category>();
            CreateMap<Category, CategoryVM>();

            CreateMap<PaymentVM, Payment>();
            CreateMap<Payment, PaymentVM>();

            CreateMap<ProductPaymentVM, ProductPayment>();
            CreateMap<ProductPayment, ProductPaymentVM>();

            CreateMap<ProductVM, Product>();
            CreateMap<Product, ProductVM>();

            CreateMap<StatusVM, Status>();
            CreateMap<Status, StatusVM>();

            CreateMap<TagVM, Tag>();
            CreateMap<Tag, TagVM>();
        }
    }
}

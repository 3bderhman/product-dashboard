using AutoMapper;
using ECommerce.BL.Helper;
using ECommerce.BL.Interface;
using ECommerce.BL.Model;
using ECommerce.DAL.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public ProductController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        [HttpGet]
        [Route("GetProducts")]
        public async Task<IActionResult> GetProducts()
        {
            var data = await unitOfWork.ProductRep.GetAllProductsAsync();
            var result = mapper.Map<IEnumerable<ProductVM>>(data);
            return Ok(new ResponsiveMessage<IEnumerable<ProductVM>>
            {
                Code = "200",
                Status = "Ok",
                Message = "Data Found",
                Data = result
            });
        }
        [HttpGet]
        [Route("GetProduct/{id}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            var data = await unitOfWork.ProductRep.GetProductAsync(x => x.Id == id);
            if(data != null)
            {
                var result = mapper.Map<ProductVM>(data);
                return Ok(new ResponsiveMessage<ProductVM>
                {
                    Code = "200",
                    Status = "Ok",
                    Message = "Data Found",
                    Data = result
                });
            }
            return BadRequest();
        }
        [HttpPost]
        [Route("AddProduct")]
        public async Task<IActionResult> AddProduct(ProductVM model)
        {
            if (ModelState.IsValid)
            {
                var result = mapper.Map<Product>(model);
                await unitOfWork.ProductRep.Create(result);
                return Ok(new ResponsiveMessage<Product>
                {
                    Code = "201",
                    Status = "Created",
                    Message = "Data saved",
                    Data = result
                });
            }
            return BadRequest();
        }
        [HttpPut]
        [Route("UpdateProduct/{id}")]
        public async Task<IActionResult> UpdateProduct(int id, ProductVM model)
        {
            if(id == model.Id)
            {
                if (ModelState.IsValid)
                {
                    var result = mapper.Map<Product>(model);
                    await unitOfWork.ProductRep.Update(result);
                    return Ok(new ResponsiveMessage<Product>
                    {
                        Code = "201",
                        Status = "Update",
                        Message = "Data saved",
                        Data = result
                    });
                }
            }
            return BadRequest();
        }
        [HttpDelete]
        [Route("DeleteProduct/{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        { 
            var data = await unitOfWork.ProductRep.GetAsync(x => x.Id == id);
            if (data != null)
            {
                await unitOfWork.ProductRep.Delete(data);
                return Ok(new ResponsiveMessage<string>
                {
                    Code = "202",
                    Status = "Accepted",
                    Message = "Data Deleted",
                    Data = "Data Deleted"
                });
            }
            return BadRequest();
        }
    }
}

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

    public class CategoryController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public CategoryController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        [HttpGet]
        [Route("GetCategories")]
        public async Task<IActionResult> GetCategories()
        {
            var data = await unitOfWork.CategoryRep.GetAllAsync();
            var result = mapper.Map<IEnumerable<CategoryVM>>(data);
            return Ok(new ResponsiveMessage<IEnumerable<CategoryVM>>
            {
                Code = "200",
                Status = "Ok",
                Message = "Data Found",
                Data = result
            });
        }
        [HttpGet]
        [Route("GetCategory/{id}")]
        public async Task<IActionResult> GetCategory(int id)
        {
            var data = await unitOfWork.CategoryRep.GetAsync(x => x.Id == id);
            var result = mapper.Map<CategoryVM>(data);
            return Ok(new ResponsiveMessage<CategoryVM>
            {
                Code = "200",
                Status = "Ok",
                Message = "Data Found",
                Data = result
            });
        }
        [HttpPost]
        [Route("AddCategory")]
        public async Task<IActionResult> AddCategory(CategoryVM model)
        {
            if (ModelState.IsValid)
            {
                var result = mapper.Map<Category>(model);
                await unitOfWork.CategoryRep.Create(result);
                return Ok(new ResponsiveMessage<Category>
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
        [Route("UpdateCategory/{id}")]
        public async Task<IActionResult> UpdateCategory(int id, CategoryVM model)
        {
            if (id == model.Id)
            {
                if (ModelState.IsValid)
                {
                    var result = mapper.Map<Category>(model);
                    await unitOfWork.CategoryRep.Update(result);
                    return Ok(new ResponsiveMessage<Category>
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
        [Route("DeleteCategory/{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var data = await unitOfWork.CategoryRep.GetAsync(x => x.Id == id);
            if (data != null)
            {
                await unitOfWork.CategoryRep.Delete(data);
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

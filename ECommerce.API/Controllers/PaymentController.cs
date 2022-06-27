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
    public class PaymentController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public PaymentController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        [HttpGet]
        [Route("GetPyments")]
        public async Task<IActionResult> GetPyments()
        {
            var data = await unitOfWork.PaymentRep.GetAllAsync();
            var result = mapper.Map<IEnumerable<PaymentVM>>(data);
            return Ok(new ResponsiveMessage<IEnumerable<PaymentVM>>
            {
                Code = "200",
                Status = "Ok",
                Message = "Data Found",
                Data = result
            });
        }
        [HttpGet]
        [Route("GetPyment/{id}")]
        public async Task<IActionResult> GetPyment(int id)
        {
            var data = await unitOfWork.PaymentRep.GetAsync(x => x.Id == id);
            var result = mapper.Map<PaymentVM>(data);
            return Ok(new ResponsiveMessage<PaymentVM>
            {
                Code = "200",
                Status = "Ok",
                Message = "Data Found",
                Data = result
            });
        }
        [HttpPost]
        [Route("AddPayment")]
        public async Task<IActionResult> AddPayment(PaymentVM model)
        {
            if (ModelState.IsValid)
            {
                var result = mapper.Map<Payment>(model);
                await unitOfWork.PaymentRep.Create(result);
                return Ok(new ResponsiveMessage<Payment>
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
        [Route("UpdatePayment/{id}")]
        public async Task<IActionResult> UpdatePayment(int id, PaymentVM model)
        {
            if (id == model.Id)
            {
                if (ModelState.IsValid)
                {
                    var result = mapper.Map<Payment>(model);
                    await unitOfWork.PaymentRep.Update(result);
                    return Ok(new ResponsiveMessage<Payment>
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
        [Route("DeletePayment/{id}")]
        public async Task<IActionResult> DeletePayment(int id)
        {
            var data = await unitOfWork.PaymentRep.GetAsync(x => x.Id == id);
            if (data != null)
            {
                await unitOfWork.PaymentRep.Delete(data);
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

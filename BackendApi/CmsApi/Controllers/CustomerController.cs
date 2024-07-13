using CmsApi.Iservices;
using CmsApi.Models;
using CmsApi.ModelsDto;
using CmsApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CmsApi.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class CustomerController : Controller
    {
        private readonly ICustomerService _customerService;
        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }
        /// <summary>
        /// TO GET ALL Customer
        /// </summary>
        /// <returns></returns>
        [HttpGet(template: "GetListCustomer", Name = "GetListCustomer")]
        public async Task<ResponseDto> GetListCustomer()
        {
            var responce = new ResponseDto();
            try
            {
                return await _customerService.GetCustomerList();
            }
            catch (Exception ex)
            {
                responce.Success = false;
                responce.Message = ex.Message;
                return responce;

            }
        }


        /// <summary>
        /// Get Details for customer BY ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        [HttpGet(template: "GeDetialesCustomer", Name = "GeDetialesCustomer")]
        public async Task<ResponseDto> GeDetialesCustomer(int customerId)
        {
            var responce = new ResponseDto();
            try
            {
                return await _customerService.GetCustomer(customerId);
            }
            catch (Exception ex)
            {
                responce.Success = false;
                responce.Message = ex.Message;
                return responce;

            }
        }

        /// <summary>
        /// Get Analytic For number customer in same Address
        /// </summary>
        /// <returns></returns>
        [HttpGet(template: "GetAnalytic", Name = "GetAnalytic")]
        public async Task<ResponseDto> GetAnalytic()
        {
            var responce = new ResponseDto();
            try
            {
                return await _customerService.GetAnalytic();
            }
            catch (Exception ex)
            {
                responce.Success = false;
                responce.Message = ex.Message;
                return responce;

            }
        }
        /// <summary>
        /// POST Data
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>

        [HttpPost(template: "AddCustomer", Name = "AddCustomer")]
        public async Task<ResponseDto> AddCustomer([FromBody] CustomerDto customerInput)
        {

            var responce = new ResponseDto();
            try
            {
                if (!ModelState.IsValid)
                {
                    responce.Message = "check from inputs";
                    responce.Status = StatusCodes.Status422UnprocessableEntity.ToString();
                    responce.Success = false;

                }
                else
                {
                    responce = await _customerService.AddCustomer(customerInput);

                    if (responce.Success)
                    {
                        Customer responseDto = (Customer)responce.Data;
                    }

                }
            }
            catch (Exception ex)
            {
                responce.Message = ex.Message;
                responce.Success = false;
                responce.Status = StatusCodes.Status500InternalServerError.ToString();

            }

            return responce;
        }
       
        
        /// <summary>
        /// Delete Data
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete(template: "DeleteCustomer", Name = "DeleteCustomer")]
        public async Task<ResponseDto> DeleteCustomer(int id)
        {
            var responce = new ResponseDto();
            try
            {
                return await _customerService.DeleteCustomer(id);
            }
            catch (Exception ex)
            {
                responce.Success = false;
                responce.Message = ex.Message;
                return responce;

            }
        }


        /// <summary>
        /// Update Data
        /// </summary>
        /// <param name="id"></param>
        /// <param name="customer"></param>
        /// <returns></returns>

        [HttpPut(template: "UpdateCustomer", Name = "UpdateCustomer")]
        public async Task<ResponseDto> UpdateCustomer([FromBody] CustomerDto customerInput, int id)
        {
            var responce = new ResponseDto();
            try
            {
                if (ModelState.IsValid)
                {
                    return await _customerService.UpdateCustomer(customerInput, id);
                }
                else
                {
                    responce.Success = false;
                    responce.Message = "Check From Inputs";
                }
                return responce;
            }
            catch (Exception ex)
            {
                responce.Success = false;
                responce.Message = ex.Message;
                return responce;
            }


        }



    }
}

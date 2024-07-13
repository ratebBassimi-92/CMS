using CmsApi.Iservices;
using CmsApi.Models;
using CmsApi.ModelsDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CmsApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : Controller
    {
        private readonly IAuthUserService _authUserService;
        public LoginController(IAuthUserService authUserService)
        {
            _authUserService = authUserService;
        }

        [HttpPost(template: "LoginUser", Name = "LoginUser")]
        public async Task<ResponseDto> LoginUser([FromBody] LoginDto input)
        {
            try
            {
               return await _authUserService.LoginUser(input);
            }
            catch (Exception ex)
            {
                return new ResponseDto()
                {
                    Message = ex.Message,
                    Success = false,
                    Status = StatusCodes.Status500InternalServerError.ToString()
                };
            }

        }

    }
}

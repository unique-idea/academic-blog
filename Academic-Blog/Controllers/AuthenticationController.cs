using Academic_Blog.Constants;
using Academic_Blog.Enums;
using Academic_Blog.PayLoad.Request;
using Academic_Blog.PayLoad.Response;
using Academic_Blog.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Academic_Blog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly ILogger<AuthenticationController> _logger;
        private readonly IAccountService _accountService;
        public AuthenticationController(IAccountService accountService,ILogger<AuthenticationController> logger) 
        { 

            _logger = logger;
            _accountService = accountService;
        }
        [HttpPost(ApiEndPointConstant.Authentication.Login)]
        public async Task<IActionResult> Login(LoginRequest loginRequest)
        {
            var loginResponse =  await _accountService.Login(loginRequest);
            if (loginResponse == null)
            {
                return Unauthorized(new ErrorResponse()
                {
                    StatusCode = StatusCodes.Status401Unauthorized,
                    Error = "Invalid User or Password",
                    TimeStamp = DateTime.Now,
                });
            }
            if(loginResponse.AccountStatus.Equals(AccountStatusEnum.INACTIVE.ToString()))
            {
                return Unauthorized(new ErrorResponse()
                {
                    StatusCode = StatusCodes.Status401Unauthorized,
                    Error = "Deactive Account",
                    TimeStamp = DateTime.Now,
                });
            }
            return Ok(loginResponse);
        }
    }
}

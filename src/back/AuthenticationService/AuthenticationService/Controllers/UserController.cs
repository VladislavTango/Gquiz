using AuthenticationApplication.User.Queries;
using AuthenticationApplication.User.Requestst;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CommonShared;

namespace AuthenticationService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBaseApi
    {   
        [HttpPost("RegistrationUser")]
        public async Task<IActionResult> RegistrationUser(RegistrationUserRequest request)
        {
            var responce = await Mediator.Send(request);
            return Ok(responce);
        }

        [HttpPost("LoginUser")]
        public async Task<IActionResult> LoginUser(LoginUserRequest request)
        {
            var responce = await Mediator.Send(request);
            return Ok(responce);
        }

        [Authorize(Roles = "User")]
        [HttpGet]
        public async Task<IActionResult> AuthorizeCheck() { 
        return Ok();
        }
    }
}

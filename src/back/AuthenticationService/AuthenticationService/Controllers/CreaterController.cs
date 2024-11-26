using AuthenticationApplication.Creater.Requests;
using CommonShared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationService.Controllers
{
    public class CreaterController:ControllerBaseApi
    {
        [HttpPost("RegistrationCreater")]
        public async Task<IActionResult> RegistrationUser(CreaterAddRequest request)
        {
            var responce = await Mediator.Send(request);
            return Ok(responce);
        }

        [HttpPost("LoginCreater")]
        public async Task<IActionResult> LoginUser(CreaterLoginRequest request)
        {
            var responce = await Mediator.Send(request);
            return Ok(responce);
        }

        [Authorize(Roles = "Creater")]
        [HttpGet]
        public async Task<IActionResult> AuthorizeCheck()
        {
            return Ok();
        }
    }
}

using CommonShared;
using EmailApplication.Requests;
using Microsoft.AspNetCore.Mvc;

namespace EmailService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBaseApi
    {

        [HttpPost]
        public async Task<IActionResult> Get([FromBody] RegistrationSendConfirmCodeRequest request)
        {
            bool responce = await Mediator.Send(request);

            return Ok(responce);
        }

    }
}

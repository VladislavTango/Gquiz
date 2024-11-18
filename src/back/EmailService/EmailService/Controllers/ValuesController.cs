using EmailApplication.Requests;
using EmailInfrastructure.Interface;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

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
            return responce == true ? Ok("всё отправилось") : BadRequest("ничего не отправилось") ;
        }

    }
}

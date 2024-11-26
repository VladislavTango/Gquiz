using CommonShared.Middlewares;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace CommonShared
{
    [Route("api/[controller]")]
    [ApiController]
    [ResponseFilter]
    public abstract class ControllerBaseApi : ControllerBase
    {
        private IMediator _mediator;

        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
    }
}

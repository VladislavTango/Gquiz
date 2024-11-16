using AuthenticationApplication.Creater.Requests;
using AuthenticationInfrastructure.Interface;
using MediatR;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace AuthenticationApplication.Creater.Handlers
{
    public class CreaterConfirmCodeHandler : IRequestHandler<CreaterConfirmCodeRequest, string>
    {
        private readonly IMailRepository _mailRepository;
        private readonly IJwtTokentService _jwtTokentService;

        public CreaterConfirmCodeHandler
            (IMailRepository mailRepository,
            IJwtTokentService jwtTokentService) 
        {
            _mailRepository = mailRepository;
            _jwtTokentService = jwtTokentService;
        }
        public async Task<string> Handle(CreaterConfirmCodeRequest request, CancellationToken cancellationToken)
        {
            if (!(await _mailRepository.SearchMailCode(request.Email) == request.Code)) return "invalid code";
              await _mailRepository.DeleteMailCode(request.Email);
            //return _jwtTokentService.GenerateToken();
        }
    }
}
//ты решил отправлять B add и login свой responce их там будут хранить и тут генерить jwt
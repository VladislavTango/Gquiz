using AuthenticationApplication.Creater.Requests;
using AuthenticationInfrastructure.Interface.Repository;
using AuthenticationInfrastructure.Interface.Service;
using AuthenticationInfrastructure.Services;
using MediatR;

namespace AuthenticationApplication.Creater.Handlers
{
    public class CreaterAddHandler : IRequestHandler<CreaterAddRequest, string>
    {
        private readonly ICreaterRepository _createrRepository;
        private readonly IMailRepository _mailRepository;
        private readonly IHttpService _httpService;
        public CreaterAddHandler
            (ICreaterRepository createrRepository,
            IMailRepository mailRepository,
            IHttpService httpService) 
        {
            _createrRepository = createrRepository;
            _mailRepository = mailRepository;
            _httpService = httpService;
        }
        public async Task<string> Handle(CreaterAddRequest request, CancellationToken cancellationToken)
        {
            if (!await _createrRepository.IsCreaterExist(request.Name, request.Email))
                throw new Exception("creater already exist");

            int Code = await _mailRepository.AddMailCode(request.Email);

            if (!await _httpService.SendEmailCode(request.Email, Code)) 
                throw new Exception("Error with MailService");


            return "всё ок";
        }
    }
}

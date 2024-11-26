using AuthenticationApplication.Creater.Requests;
using AuthenticationDomain;
using AuthenticationInfrastructure.Interface.Repository;
using AuthenticationInfrastructure.Interface.Service;
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
                throw new ValidationException("creater already exist", System.Net.HttpStatusCode.BadRequest);

            int Code = await _mailRepository.AddMailCode(request.Email);

            if (!await _httpService.SendEmailCode(request.Email, Code)) 
                throw new ValidationException("Error with MailService", System.Net.HttpStatusCode.BadRequest);


            return "всё ок";
        }
    }
}

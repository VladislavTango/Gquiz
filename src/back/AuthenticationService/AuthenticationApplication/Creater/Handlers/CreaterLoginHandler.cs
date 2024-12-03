using AuthenticationApplication.Creater.Requests;
using AuthenticationDomain;
using AuthenticationInfrastructure.Interface.Repository;
using AuthenticationInfrastructure.Interface.Service;
using AuthenticationInfrastructure.Services;
using MediatR;

namespace AuthenticationApplication.Creater.Handlers
{
    public class CreaterLoginHandler : IRequestHandler<CreaterLoginRequest, string>
    {
        private readonly ICreaterRepository _repository;
        private readonly IMailRepository _mailRepository;
        private readonly IRabbit _rabbit;
        public CreaterLoginHandler(ICreaterRepository createrRepository , IMailRepository mailRepository , IRabbit rabbit) 
        {
            _repository = createrRepository;
            _mailRepository = mailRepository;
            _rabbit = rabbit;
        }

        public async Task<string> Handle(CreaterLoginRequest request, CancellationToken cancellationToken)
        {
            var creator = await _repository.GetCreaterByEmailAsync(request.Email);
            if (creator == null)
                throw new ValidationException("Creater not found", System.Net.HttpStatusCode.BadRequest);

            if (!PasswordHasher.VerifyPassword(request.Password, creator.Password))
                throw new ValidationException("incorrect password", System.Net.HttpStatusCode.BadRequest);

            int Code = await _mailRepository.AddMailCode(request.Email);

            if (! await _rabbit.SendCodeAsync(request.Email , Code))
                throw new ValidationException("Error with MailService", System.Net.HttpStatusCode.BadRequest);

            return "всё ок";
        }
    }
}

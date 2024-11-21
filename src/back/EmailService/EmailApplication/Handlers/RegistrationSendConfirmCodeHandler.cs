using EmailApplication.Requests;
using EmailInfrastructure.Interface;
using MediatR;

namespace EmailApplication.Handlers
{
    public class RegistrationSendConfirmCodeHandler : IRequestHandler<RegistrationSendConfirmCodeRequest, bool>
    {
        private readonly IEmailService _emailService;
        public RegistrationSendConfirmCodeHandler(IEmailService emailService) 
        { 
            _emailService = emailService;
        }
        public async Task<bool> Handle(RegistrationSendConfirmCodeRequest request, CancellationToken cancellationToken)
        {
            await _emailService.SendConfirmCode(request.UserEmail, request.Code);

            return true;
        }
    }
}

using AuthenticationApplication.Creater.Requests;
using AuthenticationInfrastructure.Interface;
using AuthenticationInfrastructure.Repository;
using AuthenticationInfrastructure.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationApplication.Creater.Handlers
{
    public class CreaterLoginHandler : IRequestHandler<CreaterLoginRequest, string>
    {
        private readonly ICreaterRepository _repository;
        private readonly IMailRepository _mailRepository;
        public CreaterLoginHandler(ICreaterRepository createrRepository , IMailRepository mailRepository) 
        {
            _repository = createrRepository;
            _mailRepository = mailRepository;
        }

        public async Task<string> Handle(CreaterLoginRequest request, CancellationToken cancellationToken)
        {
            var creator = await _repository.GetCreaterByEmailAsync(request.Email);
            if (creator == null)
                throw new Exception("Creater not found");

            if (!PasswordHasher.VerifyPassword(request.Password, creator.Password))
                throw new Exception("incorrect password");

            int Code = await _mailRepository.AddMailCode(request.Email);

            if (! await HttpService.SendEmailCode(request.Email, Code))
                throw new Exception("Error with MailService");

            return "всё ок";
        }
    }
}

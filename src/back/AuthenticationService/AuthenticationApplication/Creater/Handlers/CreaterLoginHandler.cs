using AuthenticationApplication.Creater.Requests;
using AuthenticationInfrastructure.Interface.Repository;
using AuthenticationInfrastructure.Interface.Service;
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
        private readonly IHttpService _httpService;
        public CreaterLoginHandler(ICreaterRepository createrRepository , IMailRepository mailRepository , IHttpService httpService) 
        {
            _repository = createrRepository;
            _mailRepository = mailRepository;
            _httpService = httpService;
        }

        public async Task<string> Handle(CreaterLoginRequest request, CancellationToken cancellationToken)
        {
            var creator = await _repository.GetCreaterByEmailAsync(request.Email);
            if (creator == null)
                throw new Exception("Creater not found");

            if (!PasswordHasher.VerifyPassword(request.Password, creator.Password))
                throw new Exception("incorrect password");

            int Code = await _mailRepository.AddMailCode(request.Email);

            if (! await _httpService.SendEmailCode(request.Email, Code))
                throw new Exception("Error with MailService");

            return "всё ок";
        }
    }
}

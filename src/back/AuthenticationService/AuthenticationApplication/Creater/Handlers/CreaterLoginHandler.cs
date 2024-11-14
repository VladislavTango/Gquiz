using AuthenticationApplication.Creater.Requests;
using AuthenticationInfrastructure.Interface;
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
        private readonly IJwtTokentService _jwtTokentService;
        public CreaterLoginHandler(ICreaterRepository createrRepository , IJwtTokentService jwtTokentService) 
        {
            _repository = createrRepository;
            _jwtTokentService = jwtTokentService;
        }

        public async Task<string> Handle(CreaterLoginRequest request, CancellationToken cancellationToken)
        {
            var creator = await _repository.GetCreaterByEmailAsync(request.Email);
            if (creator == null)
                throw new Exception("Creater not found");

            if (!PasswordHasher.VerifyPassword(request.Password, creator.Password))
                throw new Exception("incorrect password");

            return _jwtTokentService.GenerateToken(creator.Id, creator.Name, "Creater");
        }
    }
}

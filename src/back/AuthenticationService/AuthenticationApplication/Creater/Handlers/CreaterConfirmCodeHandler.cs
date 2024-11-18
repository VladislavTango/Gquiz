﻿using AuthenticationApplication.Creater.Requests;
using AuthenticationDomain.Models;
using AuthenticationInfrastructure.Interface;
using AuthenticationInfrastructure.Services;
using MediatR;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace AuthenticationApplication.Creater.Handlers
{
    public class CreaterConfirmCodeHandler : IRequestHandler<CreaterConfirmCodeRequest, string>
    {
        private readonly IMailRepository _mailRepository;
        private readonly IJwtTokentService _jwtTokentService;
        private readonly ICreaterRepository _createrRepository;

        public CreaterConfirmCodeHandler
            (IMailRepository mailRepository,
            IJwtTokentService jwtTokentService, ICreaterRepository createrRepository) 
        {
            _mailRepository = mailRepository;
            _jwtTokentService = jwtTokentService;
            _createrRepository = createrRepository;
        }
        public async Task<string> Handle(CreaterConfirmCodeRequest request, CancellationToken cancellationToken)
        {
            if (!(await _mailRepository.SearchMailCode(request.Email) == request.Code)) return "invalid code";
 
            await _mailRepository.DeleteMailCode(request.Email);

            CreaterModel createrModel = await _createrRepository.GetCreaterByNameAsync(request.CreaterName);

            if (createrModel == null) 
            {
                createrModel = new CreaterModel
                {
                    Name = request.CreaterName,
                    Password = PasswordHasher.HashPassword(request.Password),
                    Email = request.Email
                };

                createrModel.Id = await _createrRepository.AddCreaterAsync(createrModel);

                if (createrModel.Id == Guid.Empty)
                    throw new Exception("Creater not found");
            }
            
            return _jwtTokentService.GenerateToken(createrModel.Id, request.CreaterName , "Creater");
        }
    }
}

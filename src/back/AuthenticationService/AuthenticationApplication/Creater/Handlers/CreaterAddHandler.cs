using AuthenticationApplication.Creater.Requests;
using AuthenticationDomain.Models;
using AuthenticationInfrastructure.Interface;
using AuthenticationInfrastructure.Services;
using MediatR;

namespace AuthenticationApplication.Creater.Handlers
{
    public class CreaterAddHandler : IRequestHandler<CreaterAddRequest, string>
    {
        private readonly ICreaterRepository _createrRepository;
        private readonly IJwtTokentService _jwtTokentService;
        public CreaterAddHandler(ICreaterRepository createrRepository , IJwtTokentService jwtTokentService) 
        {
            _createrRepository = createrRepository;
            _jwtTokentService = jwtTokentService;
        }
        public async Task<string> Handle(CreaterAddRequest request, CancellationToken cancellationToken)
        {
       

            if (! await _createrRepository.IsCreaterExist(request.Name , request.Email))
                throw new Exception("creater already exist");

            var Creater = new CreaterModel
            { 
                Name = request.Name,
                Password = PasswordHasher.HashPassword(request.Password),
                Email = request.Email,
            };

            Creater.Id = await _createrRepository.AddCreaterAsync(Creater);

            if (Creater.Id == Guid.Empty)
                throw new Exception("Creater not found");

            return _jwtTokentService.GenerateToken(Creater.Id , Creater.Name , "Creater");
        }
    }
}

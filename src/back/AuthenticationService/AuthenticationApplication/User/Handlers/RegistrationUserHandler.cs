using AuthenticationApplication.User.Queries;
using AuthenticationDomain.Models;
using AuthenticationInfrastructure.Interface.Repository;
using AuthenticationInfrastructure.Interface.Service;
using AuthenticationInfrastructure.Services;
using MediatR;


namespace AuthenticationApplication.User.Handlers
{
    public class RegistrationUserHandler : IRequestHandler<RegistrationUserRequest, string>
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtTokentService _jwtTokenService;
        public RegistrationUserHandler(IUserRepository userRepository, IJwtTokentService jwtTokenService)
        {
            _userRepository = userRepository;
            _jwtTokenService = jwtTokenService;
        }
        public async Task<string> Handle(RegistrationUserRequest request, CancellationToken cancellationToken)
        {
            if (await _userRepository.GetUserByNameAsync(request.UserName) != null)
                throw new Exception("this user already exists");

            UserModel user = new()
            {
                Name = request.UserName,
                Password = PasswordHasher.HashPassword(request.Password)
            };

            user.Id = await _userRepository.AddUserAsync(user);

            if (user.Id == Guid.Empty) throw new Exception("db error");

            return _jwtTokenService.GenerateToken(user.Id,user.Name,"User");
        }
    }
}

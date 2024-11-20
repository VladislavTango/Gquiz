
using AuthenticationApplication.User.Requestst;
using AuthenticationDomain.Models;
using AuthenticationInfrastructure.Interface.Repository;
using AuthenticationInfrastructure.Interface.Service;
using AuthenticationInfrastructure.Services;
using MediatR;

namespace AuthenticationApplication.User.Handlers
{
    public class LoginUserHandler : IRequestHandler<LoginUserRequest, string>
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtTokentService _jwtTokenService;
        public LoginUserHandler(IUserRepository userRepository, IJwtTokentService jwtTokenService)
        {
            _userRepository = userRepository;
            _jwtTokenService = jwtTokenService;
        }

        public async Task<string> Handle(LoginUserRequest request, CancellationToken cancellationToken)
        {
            UserModel user = await _userRepository.GetUserByNameAsync(request.UserName);

            if (user == null) 
                throw new Exception("user not found");

            if (!PasswordHasher.VerifyPassword(request.Password, user.Password))
                throw new Exception("incorrect password");

            return _jwtTokenService.GenerateToken(user.Id, user.Name, "User");
        }
    }
}

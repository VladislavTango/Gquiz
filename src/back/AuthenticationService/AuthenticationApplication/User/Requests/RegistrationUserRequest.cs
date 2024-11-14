using MediatR;

namespace AuthenticationApplication.User.Queries
{
    public class RegistrationUserRequest : IRequest<string>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}

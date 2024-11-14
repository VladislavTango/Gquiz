
using MediatR;

namespace AuthenticationApplication.User.Requestst
{
    public class LoginUserRequest : IRequest<string>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}

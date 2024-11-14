using MediatR;

namespace AuthenticationApplication.Creater.Requests
{
    public class CreaterLoginRequest : IRequest<string>
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}

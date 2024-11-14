using MediatR;

namespace AuthenticationApplication.Creater.Requests
{
    public class CreaterAddRequest : IRequest<string>
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }
}

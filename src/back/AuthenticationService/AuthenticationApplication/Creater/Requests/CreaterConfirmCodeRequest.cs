using MediatR;

namespace AuthenticationApplication.Creater.Requests
{
    public class CreaterConfirmCodeRequest : IRequest<string>
    {
        public string CreaterName {  get; set; }
        public string Password {  get; set; }
        public string Email { get; set; }
        public int Code {  get; set; }
    }
}

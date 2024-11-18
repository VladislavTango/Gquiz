
using MediatR;

namespace EmailApplication.Requests
{
    public  class RegistrationSendConfirmCodeRequest : IRequest<bool>
    {
        public string UserEmail { get; set; }
        public int Code { get; set; }
    }
}

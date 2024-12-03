using EmailDomain.Models;

namespace EmailInfrastructure.Interface
{
    public interface IRabbit
    {
        public Task SendEmailCode();
    }
}

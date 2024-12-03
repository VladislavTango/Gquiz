
namespace AuthenticationInfrastructure.Interface.Service
{
    public interface IRabbit
    {
        Task<bool> SendCodeAsync(string email, int code);
    }
}

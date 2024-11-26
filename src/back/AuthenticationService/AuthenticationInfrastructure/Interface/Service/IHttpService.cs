namespace AuthenticationInfrastructure.Interface.Service
{
    public interface IHttpService
    {
        public Task<bool> SendEmailCode(string Email, int Code);
    }
}

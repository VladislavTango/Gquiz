
namespace AuthenticationInfrastructure.Interface.Repository
{
    public interface IMailRepository
    {
        public Task<int> AddMailCode(string Email);
        public Task<bool> DeleteMailCode(string Email);
        public Task<int> SearchMailCode(string Email);
    }
}

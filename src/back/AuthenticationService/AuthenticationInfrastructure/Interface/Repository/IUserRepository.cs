using AuthenticationDomain.Models;

namespace AuthenticationInfrastructure.Interface.Repository
{
    public interface IUserRepository
    {
        Task<Guid> AddUserAsync(UserModel User);
        Task<UserModel> GetUserByNameAsync(string UserName);
    }
}

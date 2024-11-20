using AuthenticationDomain.Models;
using AuthenticationInfrastructure.AppContext;
using AuthenticationInfrastructure.Interface.Repository;
using Microsoft.EntityFrameworkCore;

namespace AuthenticationInfrastructure.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationContext _context;

        public UserRepository(ApplicationContext context)
        { 
            _context = context;
        }
        public async Task<Guid> AddUserAsync(UserModel User)
        {
            _context.User.Add(User);
            await _context.SaveChangesAsync();

            return User.Id;
        }

        public async Task<UserModel> GetUserByNameAsync(string UserName)
        {
            return await _context.User
                .Where(x => x.Name == UserName)
                .FirstOrDefaultAsync(); ;
        }
    }
}

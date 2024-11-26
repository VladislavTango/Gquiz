using AuthenticationDomain.Models;
using AuthenticationInfrastructure.AppContext;
using AuthenticationInfrastructure.Interface.Repository;
using Microsoft.EntityFrameworkCore;

namespace AuthenticationInfrastructure.Repository
{
    public class CreaterRepository : ICreaterRepository
    {
        private readonly ApplicationContext _context;
        public CreaterRepository(ApplicationContext context)
        {
            _context = context;
        }
        public async Task<Guid> AddCreaterAsync(CreaterModel creater)
        {
            _context.Creater.Add(creater);
            await _context.SaveChangesAsync();

            return creater.Id;
        }

        public async Task<CreaterModel> GetCreaterByEmailAsync(string Email)
        {
            return await _context.Creater
                .Where(x => Email == x.Email)
                .FirstOrDefaultAsync();
        }

        public async Task<CreaterModel> GetCreaterByNameAsync(string CreaterName)
        {
            return await _context.Creater
                .Where(x => CreaterName == x.Name)
                .FirstOrDefaultAsync();
        }

        public async Task<bool> IsCreaterExist(string Name, string Email)
        {
            return await _context.Creater
                .Where(x => Name == x.Name || Email == x.Email)
                .FirstOrDefaultAsync() == null ? true : false;
        }
    }
}

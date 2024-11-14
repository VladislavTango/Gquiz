using AuthenticationDomain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationInfrastructure.Interface
{
    public interface IUserRepository
    {
        Task<Guid> AddUserAsync(UserModel User);
        Task<UserModel> GetUserByNameAsync(string UserName);
    }
}

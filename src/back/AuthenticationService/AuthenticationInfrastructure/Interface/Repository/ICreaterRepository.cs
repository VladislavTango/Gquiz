using AuthenticationDomain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AuthenticationInfrastructure.Interface.Repository
{
    public interface ICreaterRepository
    {
        Task<Guid> AddCreaterAsync(CreaterModel creater);
        Task<CreaterModel> GetCreaterByNameAsync(string CreaterName);
        Task<CreaterModel> GetCreaterByEmailAsync(string Email);
        Task<bool> IsCreaterExist(string Name, string Email);
    }
}

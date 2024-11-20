using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationInfrastructure.Interface.Service
{
    public interface IHttpService
    {
        public Task<bool> SendEmailCode(string Email, int Code);
    }
}

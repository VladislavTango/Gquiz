﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationInfrastructure.Interface
{
    public interface IMailRepository
    {
        public Task AddMailCode(string Email,int Code);
        public Task<bool> DeleteMailCode(string Email);
        public Task<int> SearchMailCode(string Email);
    }
}

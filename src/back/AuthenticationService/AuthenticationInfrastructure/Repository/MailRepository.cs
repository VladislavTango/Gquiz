﻿using AuthenticationInfrastructure.Interface;
using StackExchange.Redis;
using System.Collections.Generic;
using static Org.BouncyCastle.Crypto.Engines.SM2Engine;

namespace AuthenticationInfrastructure.Repository
{
    public class MailRepository : IMailRepository
    {
        private readonly ConnectionMultiplexer redis;

        private readonly IDatabase db;

        private readonly string HashKey = "CheckMailCreator";

        public MailRepository()
        {
            redis = ConnectionMultiplexer.Connect("127.0.0.1:6379");
            db = redis.GetDatabase();
        }
        public async Task AddMailCode(string Email , int Code)
        {
            string key = $"{HashKey}:{Email}";

            var heshEntries = new HashEntry[] 
            {
                new HashEntry(Email, Code)
            };
            await db.HashSetAsync(HashKey, heshEntries);

            await db.StringSetAsync(key, Code);
            await db.KeyExpireAsync(key, TimeSpan.FromHours(2));
        }

        public async Task<bool> DeleteMailCode(string Email)
        {
            if (!await db.HashDeleteAsync(HashKey, Email)) throw new Exception("Redis hash delete error");
            if (!await db.KeyDeleteAsync($"{HashKey}:{Email}")) throw new Exception("Redis key delete error");
            
            return true;
        }

        public async Task<int> SearchMailCode(string Email)
        {
            return (int)await db.HashGetAsync(HashKey, Email);
        }
    }
}

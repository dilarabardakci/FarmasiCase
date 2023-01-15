using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FarmasiCase.Models;
using Microsoft.Extensions.Options;
using StackExchange.Redis;
using System.Text.Json;

namespace FarmasiCase.Services
{
    public class RedisService
    {
        private readonly ConnectionMultiplexer _redis;
        public RedisService(IOptions<RedisSettings> setting)
        {
             _redis = ConnectionMultiplexer.Connect(new ConfigurationOptions
            {
                EndPoints = { setting.Value.ConnectionString }
            });
            
        }


        public IDatabase GetDatabase()
        {
            return _redis.GetDatabase();
        }
    }
}

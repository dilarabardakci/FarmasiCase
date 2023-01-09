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
    public class BasketService
    {
        private readonly IDatabase _redisDb;
        public BasketService(IOptions<RedisSettings> setting)
        {
            var _redis = ConnectionMultiplexer.Connect(new ConfigurationOptions
            {
                EndPoints = {setting.Value.ConnectionString}
            });
            _redisDb = _redis.GetDatabase();
        }

        public async Task<string?> GetString (string key)
        {
            return await _redisDb.StringGetAsync(key);
        } 

        public async Task<bool> SetString (string key,string value)
        {
            return await _redisDb.StringSetAsync(key,value);
        }

        public async Task<bool> SetBasket (string userId, string[] productIds)
        {
            return await _redisDb.StringSetAsync(userId, JsonSerializer.Serialize(productIds));
        }
        public async Task<string[]> GetBasket(string userId)
        {
            var response = await _redisDb.StringGetAsync(userId);
            if (response.IsNull)
            {
                return new string[] { }; //Bos string array olusturulması
            }
            return JsonSerializer.Deserialize<string[]>(response);
        }
            


    }
}

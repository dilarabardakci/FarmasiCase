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
        public BasketService(RedisService service)
        {
            _redisDb = service.GetDatabase();
        }

        public async Task<string> GetString (string key)
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
        public void ClearBasket (string userId)
        {
            _redisDb.KeyDelete(userId);
        }
        public async Task <string[]> RemoveItem (string userId, string productId)
        {
            string[] basket = await GetBasket(userId);
            if (basket.Contains<string>(productId))
            {
                List<string> NewBasket = basket.ToList();
                NewBasket.Remove(productId);
                await SetBasket(userId,NewBasket.ToArray());
                return NewBasket.ToArray();
            }
            else
            {
                return null;
            }
        }
            


    }
}

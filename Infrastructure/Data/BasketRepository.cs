using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;
using StackExchange.Redis;


namespace Infrastructure.Data
{
    public class BasketRepository : IBasketRepository
    {
        private readonly StackExchange.Redis.IDatabase _database;
        public BasketRepository(IConnectionMultiplexer redis)
        {
            _database =redis.GetDatabase();
        }
        public async Task<bool> DeleteBasketAsync(string basketId)
        {
            return  await _database.KeyDeleteAsync(basketId);
        }

        public async Task<CustomerBasket> GetBasketAsync(string BasketId)
        {
            var data = await _database.StringGetAsync(BasketId);
            return data.IsNullOrEmpty?null:JsonSerializer.Deserialize<CustomerBasket>(data);
        }

        public async Task<CustomerBasket> UpdateBasketAsync(CustomerBasket basket)
        {
            var created = await _database.StringSetAsync(basket.Id, JsonSerializer.Serialize(basket),TimeSpan.FromDays(30));
            if(!created) return null ;
            return await GetBasketAsync(basket.Id);
        }
    }
}
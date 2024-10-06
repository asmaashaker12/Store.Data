using StackExchange.Redis;
using Store.Repository.Basket.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;

namespace Store.Repository.Basket
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDatabase _database;
        public BasketRepository(IConnectionMultiplexer redis)
        {
            _database=redis.GetDatabase();
        }
        public async Task<bool> DeleteBasketAsync(string basketId)
        =>await _database.KeyDeleteAsync(basketId);
            
        

        public async Task<CustomerBasket> GetCustomerAsync(string basketId)
        {
            var basket=await _database.StringGetAsync(basketId);
            return basket.IsNullOrEmpty?null:JsonSerializer.Deserialize<CustomerBasket>(basket);
        }

        public async Task<CustomerBasket> UpdateBasketAsync(CustomerBasket customerBasket)
        {
            var isCreated = await _database.StringSetAsync(customerBasket.Id, JsonSerializer.Serialize(customerBasket), TimeSpan.FromDays(30));
            if (!isCreated)
                return null;
            return await GetCustomerAsync(customerBasket.Id);   
        }

        Task<CustomerBasket> IBasketRepository.GetCustomerAsync(string basketId)
        {
            throw new NotImplementedException();
        }

        Task<CustomerBasket> IBasketRepository.UpdateBasketAsync(CustomerBasket customerBasket)
        {
            throw new NotImplementedException();
        }
    }
}

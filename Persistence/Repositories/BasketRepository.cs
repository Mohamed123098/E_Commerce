using Domain.Contracts;
using Domain.Models.BasketModule;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class BasketRepository(IConnectionMultiplexer connection) : IBasketRepository
    {
        private readonly IDatabase _database = connection.GetDatabase();
        public async Task<CustomerBasket?> CreateOrUpdateAsync(CustomerBasket? basket,TimeSpan? time=null)
        {
            if(basket is not null)
            {
                var basketObject = JsonSerializer.Serialize(basket);
              var IsCreatedOrUpdated= await _database.StringSetAsync(basket.Id,basketObject,time??TimeSpan.FromDays(30));
                if (IsCreatedOrUpdated)
                    return await GetBasketAsync(basket.Id);
            }
            return null;
        }

        public async Task<bool> DeleteBasketAsync(string id)
        =>await _database.KeyDeleteAsync(id);


        public async Task<CustomerBasket?> GetBasketAsync(string id)
        {
            var basketJSON =await _database.StringGetAsync(id);
            CustomerBasket? basket = JsonSerializer.Deserialize<CustomerBasket>(basketJSON);
            return basket;
            
        }
        
    }
}

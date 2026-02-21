using Domain.Models.BasketModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts
{
    public interface ICustomerBasket
    {
        Task<CustomerBasket?> GetBasketAsync(string id);
        Task<CustomerBasket?> CreateOrUpdateAsync(CustomerBasket basket,TimeSpan? time);
        Task<bool> DeleteBasketAsync(string id);

    }
}

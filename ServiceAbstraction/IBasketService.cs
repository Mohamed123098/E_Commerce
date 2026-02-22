using Shared.DTOs.BasketDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAbstraction
{
    public interface IBasketService
    {
        Task<CustomerBasketDTO?> GetBasketAsync(string id);
        Task<CustomerBasketDTO> CreateOrUpdateAsync(CustomerBasketDTO basketCustomer);
        Task<bool> DeleteBasketAsync(string id);
    }
}

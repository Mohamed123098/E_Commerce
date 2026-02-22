using AutoMapper;
using Domain.Contracts;
using Domain.Exceptions.BasketExcep;
using Domain.Models.BasketModule;
using ServiceAbstraction;
using Shared.DTOs.BasketDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceImplementation
{
    public class BasketService(IBasketRepository repository,IMapper _mapper) : IBasketService
    {
        public async Task<CustomerBasketDTO?> CreateOrUpdateAsync(CustomerBasketDTO basketCustomer)
        {
            var basket =  _mapper.Map<CustomerBasketDTO,CustomerBasket>(basketCustomer);
            //var basket = new CustomerBasket() { Id = basketCustomer.Id, };//BasketItems =basketCustomer.BasketItemsDTO };
            if (basket is not null)
            {
               var createdOrUpdatedBasket = await repository.CreateOrUpdateAsync(basket, TimeSpan.FromDays(30));
                if (createdOrUpdatedBasket != null)
                    return await GetBasketAsync(basketCustomer.Id);
                else
                    throw new Exception($"Can't create or updated basket with id {basket.Id}");
            }
            throw new Exception($"Can't create or update customer basket with id {basketCustomer.Id}");
        }


        public async Task<CustomerBasketDTO?> GetBasketAsync(string id)
        {
            CustomerBasketDTO customerBasketDTO;
            var basket =await repository.GetBasketAsync(id);
            if(basket is not null)
            {
                customerBasketDTO = _mapper.Map<CustomerBasket, CustomerBasketDTO>(basket);
                return customerBasketDTO;
            }
            throw new CustomerBasketNotFound(id);
        }
        public async Task<bool> DeleteBasketAsync(string id) => await repository.DeleteBasketAsync(id);

   
    }
}

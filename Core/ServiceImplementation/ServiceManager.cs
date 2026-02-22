using AutoMapper;
using Domain.Contracts;
using Domain.Models.BasketModule;
using ServiceAbstraction;
using ServiceImplementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceImplementation
{
    public class ServiceManager(IUnitOfWork unitOfWork,IMapper mapper,IBasketRepository repository) : IServiceManager
    {
        private readonly Lazy<IProductService> productService =new Lazy<IProductService>(()=>new ProductService(unitOfWork,mapper));
        public IProductService ProductService => productService.Value;
        private readonly Lazy<IBasketService> customerBasket = new Lazy<IBasketService>(()=>new BasketService(repository,mapper));
        public IBasketService BasketService => customerBasket.Value;
    }
}

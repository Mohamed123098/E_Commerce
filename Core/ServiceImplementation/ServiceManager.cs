using AutoMapper;
using Domain.Contracts;
using Domain.Models.BasketModule;
using ServiceAbstraction;
using ServiceImplementation.BasketCustomerService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceImplementation
{
    public class ServiceManager(IUnitOfWork unitOfWork,IMapper mapper,ICustomerBasket customerBasket) : IServiceManager
    {
        private readonly Lazy<IProductService> productService =new Lazy<IProductService>(()=>new ProductService.ProductService(unitOfWork,mapper));
        public IProductService ProductService => productService.Value;
        private readonly Lazy<ICustomerBasket> customerBasket = new Lazy<ICustomerBasket>(()=>new BasketCustomerService.BasketCustomerService(customerBasket,mapper));
        public ICustomerBasket CustomerBasketService => customerBasket.Value;
    }
}

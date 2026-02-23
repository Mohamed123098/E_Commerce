using AutoMapper;
using Domain.Contracts;
using Domain.Models.BasketModule;
using Domain.Models.IdentityModule;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using ServiceAbstraction;
using ServiceImplementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceImplementation
{
    public class ServiceManager(IUnitOfWork unitOfWork,IMapper mapper,IBasketRepository repository,UserManager<ApplicationUser> userManager,IConfiguration configuration) : IServiceManager
    {
        private readonly Lazy<IProductService> productService =new Lazy<IProductService>(()=>new ProductService(unitOfWork,mapper));
        public IProductService ProductService => productService.Value;
        private readonly Lazy<IBasketService> customerBasket = new Lazy<IBasketService>(()=>new BasketService(repository,mapper));
        public IBasketService BasketService => customerBasket.Value;


        private readonly Lazy<IAccountService> accountService = new Lazy<IAccountService>(new Account.AccountService(userManager,configuration,mapper));
        public IAccountService AccountService => accountService.Value;

    }
}

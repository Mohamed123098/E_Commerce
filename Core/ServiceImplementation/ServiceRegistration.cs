using Domain.Contracts;
using Domain.Models.BasketModule;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ServiceAbstraction;
using ServiceImplementation.Account;
using ServiceImplementation.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceImplementation
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddServicesOfImplementationLayer(this IServiceCollection services,IConfiguration Configuration)
        {
            services.AddAutoMapper(x => x.AddProfile(new ProductProfile(Configuration)));
            services.AddAutoMapper(x => x.AddProfile(new BasketProfile()));
            services.AddAutoMapper(x => x.AddProfile(new AccountProfile()));
            services.AddAutoMapper(x => x.AddProfile(new AddressOrderProfile()));
            
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<Func<IProductService> >(provider=>
                () => provider.GetRequiredService<IProductService>());

            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<Func<IOrderService>>(provider => () => 
            provider.GetRequiredService<IOrderService>());

            services.AddScoped<IBasketService, BasketService>();
            services.AddScoped<Func<IBasketService>>(provider => () =>
            provider.GetRequiredService<IBasketService>());

            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<Func<IAccountService>>(provider => () => 
            provider.GetRequiredService<IAccountService>());
            services.AddScoped<IServiceManager, ServiceManagerWithFactoryDelegate>();
            services.AddScoped<IAccountService, AccountService>();
            //services.AddScoped<IBasketCustomerService, BasketCustomerService>();
            
            return services;
        }
    }
}

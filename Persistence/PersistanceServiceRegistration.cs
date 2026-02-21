using Domain.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persentation.Data;
using Persistence.Repositories;
using ServiceAbstraction;
using ServiceImplementation.BasketCustomerService;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence
{
    public static class PersistanceServiceRegistration
    {
        public static IServiceCollection AddServicesOfPersistanceLayer(this IServiceCollection services,IConfiguration Configuration)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ICustomerBasketService, GenericBasketCustomer>();
            services.AddSingleton<IConnectionMultiplexer>(
                (_) => ConnectionMultiplexer.Connect(Configuration.GetConnectionString("RedisConnectionString")
                ));
            services.AddDbContext<StoreDbContext>(
                options =>
                {
                    options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
                }
                );
            services.AddScoped<IDataSeeding, DataSeed>();
            services.AddScoped<IBasketCustomerService, BasketCustomerService>();
            return services;
        }
       
    }
}

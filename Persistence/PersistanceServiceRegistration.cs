using Domain.Contracts;
using Domain.Models.IdentityModule;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persentation.Data;
using Persistence.Identity;
using Persistence.Repositories;
using ServiceAbstraction;
using ServiceImplementation;
using StackExchange.Redis;
using System;
using Microsoft.IdentityModel.Tokens;
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
            services.AddScoped<IBasketRepository, Repositories.BasketRepository>();
            services.AddSingleton<IConnectionMultiplexer>(
                (_) => ConnectionMultiplexer.Connect(Configuration.GetConnectionString("RedisConnectionString")
                ));
            services.AddDbContext<StoreDbContext>(
                options =>
                {
                    options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
                }
                );
            services.AddIdentityCore<ApplicationUser>(options =>
            {
                options.User.RequireUniqueEmail = true;//to prevent to repeat email
                //options.User.AllowedUserNameCharacters;
            })
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<IdentityStoreDbContext>();
            services.AddScoped<IDataSeeding, DataSeed>();
            services.AddDbContext<IdentityStoreDbContext>(options=>
            {
                options.UseSqlServer(Configuration.GetConnectionString("IdentityConnectionString"));
            });
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(
                options =>
                {
                options.TokenValidationParameters = new()
                {
                    ValidateIssuer = true,
                    ValidIssuer = Configuration["JWT:Issuer"],
                    ValidateAudience = true,
                    ValidAudience = Configuration["JWT:Audiance"],
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:SecreteKey"]))
                    };
                });
            return services;
        }
       
    }
}


using Domain.Contracts;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens.Experimental;
using Persentation.Data;
using Persistence;
using Persistence.Repositories;
using ServiceAbstraction;
using ServiceImplementation;
using ServiceImplementation.Mapping;
using Shared.ErrorModels;
using System.Linq;
using System.Threading.Tasks;
using Website.CustomMiddelWares;
using Website.ServicesRegistration;

namespace Website
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Logging.AddFilter(
                     "LuckyPennySoftware.AutoMapper.License",
                 LogLevel.None);
            #region ServicesAdded
            builder.Services.AddServicesOfImplementationLayer(builder.Configuration);
            #endregion
            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();
            #region Custom Validation
            builder.Services.AddServicesOfGeneralNotSpecific();
            #endregion
            builder.Services.AddServicesOfPersistanceLayer(builder.Configuration);
            var app = builder.Build();
            //-----------------------------------------------------------
            await app.AddSeddingConfiguration();
            //---------------------------------------------------------------
            // Configure the HTTP request pipeline.
            #region ExceptionMiddleWare
            app.AddExceptionMiddleWare();
            #endregion
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }
            app.UseStaticFiles();
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}

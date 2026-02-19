
using Domain.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Persentation.Data;
using Persistence;
using Persistence.Repositories;
using ServiceAbstraction;
using ServiceImplementation;
using ServiceImplementation.Mapping;
using System.Threading.Tasks;

namespace Website
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            #region ServicesAdded
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddAutoMapper(x => x.AddProfile(new ProductProfile(builder.Configuration)));
            builder.Services.AddScoped<IServiceManager, ServiceManager>(); 
            #endregion
            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();
            builder.Services.AddDbContext<StoreDbContext>(
                options =>
                {
                    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
                }
                );
            builder.Services.AddScoped<IDataSeeding, DataSeed>();
            var app = builder.Build();
           //-----------------------------------------------------------
            var scoppe = app.Services.CreateScope();
            var obj = scoppe.ServiceProvider.GetRequiredService<IDataSeeding>();
           await obj.DataSeedDataAsync();
         //---------------------------------------------------------------
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }
            app.UseStaticFiles();
            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}

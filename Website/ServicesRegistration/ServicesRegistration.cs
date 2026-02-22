using Domain.Contracts;
using Microsoft.AspNetCore.Mvc;
using Shared.ErrorModels;
using Website.CustomMiddelWares;

namespace Website.ServicesRegistration
{
    public static class ServicesRegistration
    {
        public static void AddServicesOfGeneralNotSpecific(this IServiceCollection Services)
        {
            Services.Configure<ApiBehaviorOptions>(option =>
            {
                option.InvalidModelStateResponseFactory = (Context) =>
                {
                    var Errors = Context.ModelState.Where(M => M.Value.Errors.Any()).Select(M => new ValidationErrors() { Field = M.Key, Errors = M.Value.Errors.Select(M => M.ErrorMessage) });
                    var response = new ValidationErrorModel()
                    {
                        ValidationErrors = Errors,
                        StatusCode = 404,
                        Message = "Vaildation Query params",

                    };
                    return new BadRequestObjectResult(response);
                };
            });

        }
        public static async Task AddSeddingConfiguration(this WebApplication app)
        {
            var scoppe = app.Services.CreateScope();
            var obj = scoppe.ServiceProvider.GetRequiredService<IDataSeeding>();
            await obj.DataSeedDataAsync();
            await obj.IdentityDataSeedDataAsync();

        }
        public static void AddExceptionMiddleWare(this WebApplication app)
        {
            app.UseMiddleware<CustomExceptionHandlerMiddleWare>();
        }
    }
}

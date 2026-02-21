using Domain.Exceptions.ProductExceptions;
using Microsoft.OpenApi.MicrosoftExtensions;
using Shared.ErrorModels;

namespace Website.CustomMiddelWares
{
    public class CustomExceptionHandlerMiddleWare
    {
        private readonly RequestDelegate _next;
        //private readonly ILogger _logger;

        public CustomExceptionHandlerMiddleWare(RequestDelegate Next)
        {
            _next = Next;
            
        }
        public async Task InvokeAsync(HttpContext httpContext)
        {
            await ExceptionHandling(httpContext);
        }

        private async Task ExceptionHandling(HttpContext httpContext)
        {
            try
            {

                await _next.Invoke(httpContext);
                await NotFoundEndPoint(httpContext);
            }
            catch (Exception ex)
            {
                //#region Logger
                // _logger.LogError("internal server error ==>500");
                //#endregion
                #region Switch Expression
                httpContext.Response.StatusCode = ex switch
                {
                    NotFoundException => StatusCodes.Status404NotFound,
                    //Default

                    _ => StatusCodes.Status500InternalServerError
                }; 
                #endregion
                var exception = new ErrorToReturn()
                {
                    StatusCode = httpContext.Response.StatusCode,
                    ErrorMessage = ex.Message
                };


                await httpContext.Response.WriteAsJsonAsync(exception);
            }
        }

        private static async Task NotFoundEndPoint(HttpContext httpContext)
        {
            if (httpContext.Response.StatusCode == StatusCodes.Status404NotFound)
            {
                var errorToReturn = new ErrorToReturn()
                {
                    StatusCode = 404,
                    ErrorMessage = $"End Point {httpContext.Request.Path} is not Found"
                };
                await httpContext.Response.WriteAsJsonAsync(errorToReturn);
            }
        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using ServiceAbstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persentation.CustomAttributes
{
    internal class CacheAttribute(int Duration=90):ActionFilterAttribute
    {
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            string cacheKey = CreateCacheKey(context.HttpContext.Request);

            var cacheService = context.HttpContext.RequestServices.GetRequiredService<ICacheService>();
            string cacheValue =await cacheService.GetAsync(cacheKey);
            if(!string.IsNullOrEmpty(cacheValue))
            {
                context.Result = new ContentResult()
                {
                    Content = cacheValue,
                    ContentType="application/json",
                    StatusCode=StatusCodes.Status200OK                 
                };
                return;
            }
          var executedContext = await next.Invoke();
            if(executedContext.Result is OkObjectResult result)
            {
                await cacheService.SetAsync(cacheKey,result.Value.ToString(), TimeSpan.FromSeconds(Duration));

            }
        }

        private string CreateCacheKey(HttpRequest request)
        {
            StringBuilder Key = new();
            Key.Append(request.Path+'?');
            foreach (var item in request.Query.OrderBy(o=>o.Key))
            {
                Key.Append($"{item.Key}={item.Value}&");

            }
                return Key.ToString();
        }
    }
}

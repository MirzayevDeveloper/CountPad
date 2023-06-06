using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Distributed;

namespace CountPad.Api.Filters
{
    public class RedisCacheAttribute : ActionFilterAttribute
    {
        private static IDistributedCache _cache;
        private readonly int _absoluteDuration;
        private readonly int _slidingDuration;

        public RedisCacheAttribute(int absoluteDuration, int slidingDuration)
        {
            _absoluteDuration = absoluteDuration;
            _slidingDuration = slidingDuration;
        }

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            _cache = context.HttpContext.RequestServices.GetRequiredService<IDistributedCache>();
            string key = GetCacheKey(context);

            var cachedResult = await _cache.GetStringAsync(key);

            if (cachedResult != null)
            {
                context.Result = new ContentResult
                {
                    Content = cachedResult,
                    ContentType = "application/json",
                    StatusCode = 200
                };
            }
            else
            {
                var resultContext = await next();

                if (resultContext.Result is ContentResult result)
                {
                    await _cache.SetStringAsync(key, result.Content, new DistributedCacheEntryOptions
                    {
                        SlidingExpiration = TimeSpan.FromSeconds(_slidingDuration),
                        AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(_absoluteDuration)
                    });

                    context.Result = result;
                }
            }
        }

        private string GetCacheKey(ActionExecutingContext context)
        {
            var controllerName = context.Controller.GetType().FullName;
            var actionName = context.ActionDescriptor.DisplayName;
            var parameters = context.ActionArguments;

            return $"{controllerName}_{actionName}_{parameters}";
        }
    }
}

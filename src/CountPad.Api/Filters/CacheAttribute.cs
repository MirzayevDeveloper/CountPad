using LazyCache;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Memory;

namespace CountPad.Api.Filters
{
    public class CacheAttribute : ActionFilterAttribute
    {
        private static IAppCache? _cache;
        private int _absoluteDuration;
        private int _slidingDuration;

        public CacheAttribute(int absoluteDuration, int slidingDuration)
        {
            _absoluteDuration = absoluteDuration;
            _slidingDuration = slidingDuration;
        }

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            _cache = context.HttpContext.RequestServices.GetRequiredService<IAppCache>();
            string key = GetCacheKey(context);

            var result = await _cache.GetOrAddAsync(key, () => next(),
                new MemoryCacheEntryOptions
                {
                    SlidingExpiration = TimeSpan.FromSeconds(_slidingDuration),
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(_absoluteDuration)
                });

            context.Result = result.Result;
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

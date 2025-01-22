using System.Diagnostics;

namespace Restaurants.API.Middlewares;
public class RequestTimeHandlingMiddle(ILogger<RequestTimeHandlingMiddle> logger) : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var stopWatching = Stopwatch.StartNew();

        await next.Invoke(context);
        stopWatching.Stop();

        if (stopWatching.ElapsedMilliseconds / 1000 > 4)
        {
            logger.LogInformation("Request [{Verb}] at {Path} took {Time} ms",
                context.Request.Method,
                context.Request.Path,
                stopWatching.ElapsedMilliseconds / 1000);
        }
    }

}

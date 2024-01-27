using CompleteDevNet.API.Middleware;

namespace CompleteDevNet.API;

public static class Startup
{
    public static IApplicationBuilder UseGlobalExceptionHandler(this IApplicationBuilder app)
        => app.UseMiddleware<ExceptionMiddleware>();
}

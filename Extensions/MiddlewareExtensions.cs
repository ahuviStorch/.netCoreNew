using SweetBakery.Middleware;

namespace SweetBakery.Extensions;

public static class MiddlewareExtensions
{
    public static WebApplication UseApplicationMiddleware(
        this WebApplication app)
    {
        app.UseMiddleware<ExceptionHandlingMiddleware>();

        app.UseMiddleware<RequestLoggingMiddleware>();

        return app;
    }
}

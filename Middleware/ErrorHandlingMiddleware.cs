using System.Text.Json;

namespace SweetBakery.Middleware;

public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ErrorHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            Console.WriteLine("ERROR: " + ex.Message);

            if (context.Response.HasStarted)
            {
                throw;
            }

            context.Response.Clear();
            context.Response.StatusCode = 500;
            context.Response.ContentType = "application/json";

            var error = new
            {
                error = "Internal server error",
                status = 500
            };

            string json = JsonSerializer.Serialize(error);

            await context.Response.WriteAsync(json);
        }
    }
}
namespace InternetBank.Core.Api.Middlewares;

public class HalfErrorMiddleware
{
    public async Task InvokeAsync(HttpContext context, IHttpClientFactory httpClientFactory)
    {
        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        await context.Response.WriteAsync("Service temporarily unavailable. Please try again later.");
        return;
    }
}
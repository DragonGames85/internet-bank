using System;

namespace CreditService.Middlewares;

public class HalfErrorMiddleware
{
    private readonly RequestDelegate _next;

    public HalfErrorMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, IHttpClientFactory httpClientFactory)
    {
        var random = new Random();

        if (random.NextDouble() < 0.5)
        {
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            await context.Response.WriteAsync("Something went wrong.");
            return;
        }

        await _next(context);
    }
}
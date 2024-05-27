namespace InternetBank.Core.Api.Middlewares;

public class CircuitBreakerMiddleware
{
    private readonly RequestDelegate _next;
    private static CircuitBreakerState _state = CircuitBreakerState.Closed;
    private static int _failureCount = 0;
    private static readonly int _failureThreshold = 3;
    private static readonly TimeSpan _breakDuration = TimeSpan.FromSeconds(30);
    private static DateTime _lastFailureTime;

    public CircuitBreakerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, IHttpClientFactory httpClientFactory)
    {
        if (_state == CircuitBreakerState.Open && DateTime.UtcNow - _lastFailureTime < _breakDuration)
        {
            context.Response.StatusCode = StatusCodes.Status503ServiceUnavailable;
            await context.Response.WriteAsync("Service temporarily unavailable. Please try again later.");
            return;
        }

        if (_state == CircuitBreakerState.Open)
        {
            _state = CircuitBreakerState.HalfOpen;
        }

        try
        {
            var client = httpClientFactory.CreateClient("ExternalApi");
            var response = await client.GetAsync("/endpoint"); // Example endpoint

            if (response.IsSuccessStatusCode)
            {
                _state = CircuitBreakerState.Closed;
                _failureCount = 0;
                await _next(context);
            }
            else
            {
                throw new HttpRequestException("Non-success status code");
            }
        }
        catch (Exception ex)
        {
            _failureCount++;
            if (_failureCount >= _failureThreshold)
            {
                _state = CircuitBreakerState.Open;
                _lastFailureTime = DateTime.UtcNow;
            }
            else if (_state == CircuitBreakerState.HalfOpen)
            {
                _state = CircuitBreakerState.Open;
                _lastFailureTime = DateTime.UtcNow;
            }

            context.Response.StatusCode = StatusCodes.Status503ServiceUnavailable;
            await context.Response.WriteAsync("Service temporarily unavailable. Please try again later.");
        }
    }
}

public enum CircuitBreakerState
{
    Closed,
    Open,
    HalfOpen
}
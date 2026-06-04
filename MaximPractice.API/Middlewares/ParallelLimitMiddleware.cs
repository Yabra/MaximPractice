namespace MaximPractice.API.Middlewares;

public class ParallelLimitMiddleware
{
    private readonly RequestDelegate _next;
    private static int _currentCount = 0;
    private static readonly object _lock = new();

    public static int Limit = 2;

    public ParallelLimitMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var isUnavailable = false;

        lock (_lock)
        {
            if (_currentCount >= Limit)
            {
                isUnavailable = true;
            }
            else
            {
                _currentCount++;
            }

        }

        if (isUnavailable)
        {
            context.Response.StatusCode = StatusCodes.Status503ServiceUnavailable;
            await context.Response.WriteAsync("Service is unavailable");
            return;
        }

        try
        {
            await _next(context);
        }

        finally
        {
            lock (_lock)
            {
                _currentCount--;
            }
        }
    }
}

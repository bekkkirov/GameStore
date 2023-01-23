using System.Diagnostics;

namespace GameStore.API.Middleware;

public class PerformanceTrackingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<PerformanceTrackingMiddleware> _logger;

    public PerformanceTrackingMiddleware(RequestDelegate next, ILogger<PerformanceTrackingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var timer = new Stopwatch();
        timer.Start();

        await _next.Invoke(context);

        timer.Stop();

        _logger.LogInformation("Elapsed time: {ElapsedMilliseconds} ms.", timer.ElapsedMilliseconds);
    }
}
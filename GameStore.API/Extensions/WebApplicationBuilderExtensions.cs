using GameStore.API.Middleware;
using Serilog;
using Serilog.Events;
using Serilog.Filters;

namespace GameStore.API.Extensions;

public static class WebApplicationBuilderExtensions
{
    public static void AddSerilog(this WebApplicationBuilder builder)
    {
        var logger = new LoggerConfiguration().MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
                                              .WriteTo.Logger(l =>
                                              {
                                                  l.WriteTo.Console();
                                                  l.Filter.ByExcluding(Matching.FromSource<RequestMiddleware>());
                                              })
                                              .WriteTo.Logger(l =>
                                              {
                                                  l.WriteTo.File("logs/requests.txt");
                                                  l.Filter.ByIncludingOnly(Matching.FromSource<RequestMiddleware>());
                                              })
                                              .CreateLogger();

        builder.Logging.ClearProviders();
        builder.Logging.AddSerilog(logger);

    }
}
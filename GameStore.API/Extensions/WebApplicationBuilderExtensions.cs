using System.Diagnostics;
using GameStore.API.Middleware;
using Serilog;
using Serilog.Events;
using Serilog.Filters;

namespace GameStore.API.Extensions;

public static class WebApplicationBuilderExtensions
{
    public static void AddSerilog(this WebApplicationBuilder builder)
    {
        var logger = new LoggerConfiguration().ReadFrom.Configuration(builder.Configuration).CreateLogger();

        builder.Logging.ClearProviders();
        builder.Logging.AddSerilog(logger);
    }
}
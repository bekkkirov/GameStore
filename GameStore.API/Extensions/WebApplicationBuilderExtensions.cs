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
        builder.Host.UseSerilog((context, services, configuration) => 
            configuration.ReadFrom.Configuration(context.Configuration)
                         .ReadFrom.Services(services)
                         .Enrich.FromLogContext());
    }
}
﻿using GameStore.API.Middleware;
using GameStore.Application.Persistence;
using GameStore.Infrastructure.Persistence;
using GameStore.Infrastructure.Persistence.Seed;

namespace GameStore.API.Extensions;

public static class WebApplicationExtensions
{
    public static async Task SeedDatabase(this WebApplication app)
    {
        using (var scope = app.Services.CreateScope())
        {
            await DatabaseSeeder.SeedDatabase(scope.ServiceProvider.GetRequiredService<GameStoreContext>(),
                scope.ServiceProvider.GetRequiredService<IUnitOfWork>());
        }
    }

    public static void UseRequestMiddleware(this WebApplication app)
    {
        app.UseMiddleware<RequestMiddleware>();
    }

    public static void UsePerformanceTrackingMiddleware(this WebApplication app)
    {
        app.UseMiddleware<PerformanceTrackingMiddleware>();
    }
}
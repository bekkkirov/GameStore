using GameStore.Application.Persistence;
using GameStore.Application.Persistence.Repositories;
using GameStore.Infrastructure.Persistence;
using GameStore.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace GameStore.API.Extensions;

public static class ServicesExtensions
{
    public static void AddGameStoreContext(this IServiceCollection services, IConfiguration config)
    {
        services.AddDbContext<GameStoreContext>(opt => opt.UseSqlServer(config.GetConnectionString("GameStore")));
    }
}
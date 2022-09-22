using GameStore.Application.Interfaces;
using GameStore.Application.Persistence;
using GameStore.Application.Persistence.Repositories;
using GameStore.Infrastructure.Persistence;
using GameStore.Infrastructure.Persistence.Repositories;
using GameStore.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;

namespace GameStore.API.Extensions;

public static class ServicesExtensions
{
    public static void AddGameStoreContext(this IServiceCollection services, IConfiguration config)
    {
        services.AddDbContext<GameStoreContext>(opt => opt.UseSqlServer(config.GetConnectionString("GameStore")));
    }

    public static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IGameRepository, GameRepository>();
        services.AddScoped<IGenreRepository, GenreRepository>();
        services.AddScoped<IPlatformTypeRepository, PlatformTypeRepository>();
        services.AddScoped<ICommentRepository, CommentRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }

    public static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<ICommentService, CommentService>();
        services.AddScoped<IGameService, GameService>();
    }
}
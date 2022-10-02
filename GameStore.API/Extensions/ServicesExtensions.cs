using FluentValidation;
using FluentValidation.AspNetCore;
using GameStore.Application.Interfaces;
using GameStore.Application.Options;
using GameStore.Application.Persistence;
using GameStore.Application.Persistence.Repositories;
using GameStore.Application.Validation;
using GameStore.Infrastructure.Mapping;
using GameStore.Infrastructure.Persistence;
using GameStore.Infrastructure.Persistence.Repositories;
using GameStore.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;

namespace GameStore.API.Extensions;

public static class ServicesExtensions
{
    public static void AddGameStoreContext(this IServiceCollection services, DbConnectionOptions options)
    {
        services.AddDbContext<GameStoreContext>(opt => opt.UseSqlServer(options.GameStore));
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

    public static void AddFluentValidators(this IServiceCollection services)
    {
        services.AddFluentValidationAutoValidation();
        services.AddValidatorsFromAssemblyContaining<GameCreateValidator>();
    }

    public static void AddAutoMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(MapperProfile).Assembly);
    }
}
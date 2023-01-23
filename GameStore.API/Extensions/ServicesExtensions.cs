using System.Text;
using FluentValidation;
using FluentValidation.AspNetCore;
using GameStore.Application.Interfaces;
using GameStore.Application.Options;
using GameStore.Application.Persistence;
using GameStore.Application.Persistence.Repositories;
using GameStore.Application.Validation;
using GameStore.Infrastructure.Identity.Entities;
using GameStore.Infrastructure.Identity.Persistence;
using GameStore.Infrastructure.Mapping;
using GameStore.Infrastructure.Persistence;
using GameStore.Infrastructure.Persistence.Repositories;
using GameStore.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace GameStore.API.Extensions;

public static class ServicesExtensions
{
    public static void AddGameStoreContext(this IServiceCollection services, DbConnectionOptions options)
    {
        services.AddDbContext<GameStoreContext>(opt => opt.UseSqlServer(options.GameStore));

        services.AddDbContext<IdentityContext>(opt => opt.UseSqlServer(options.Identity));
    }

    public static void AddRepositories(this IServiceCollection services)
    {
        services.AddTransient<IGameRepository, GameRepository>();
        services.AddTransient<IGenreRepository, GenreRepository>();
        services.AddTransient<IPlatformTypeRepository, PlatformTypeRepository>();
        services.AddTransient<ICommentRepository, CommentRepository>();
        services.AddTransient<IUserRepository, UserRepository>();
        services.AddTransient<IImageRepository, ImageRepository>();
        services.AddTransient<IOrderRepository, OrderRepository>();
        services.AddTransient<ICartRepository, CartRepository>();
        services.AddTransient<IUnitOfWork, UnitOfWork>();
    }

    public static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<ICommentService, CommentService>();
        services.AddScoped<IGameService, GameService>();
        services.AddScoped<IImageService, ImageService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IOrderService, OrderService>();
        services.AddScoped<ICartService, CartService>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IAuthenticationService, AuthenticationService>();
        services.AddScoped<ICurrentUserService, CurrentUserService>();
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

    public static void AddApplicationOptions(this IServiceCollection services, IConfiguration config)
    {
        services.Configure<CloudinaryOptions>(config.GetSection(CloudinaryOptions.SectionName));
        services.Configure<JwtOptions>(config.GetSection(JwtOptions.SectionName));
    }

    public static void AddJwt(this IServiceCollection services, JwtOptions options)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(opt =>
                {
                    opt.RequireHttpsMetadata = false;
                    opt.TokenValidationParameters = new TokenValidationParameters()
                    {
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.Key)),
                        ValidateIssuerSigningKey = true,

                        ValidIssuer = options.Issuer,
                        ValidateIssuer = true,

                        ValidAudience = options.Audience,
                        ValidateAudience = true,

                        ClockSkew = TimeSpan.Zero,
                        ValidateLifetime = true,
                    };
                });
    }
    
    public static void AddIdentity(this IServiceCollection services)
    {
        services.AddIdentityCore<UserIdentity>(opt => 
                {
                    opt.User.RequireUniqueEmail = true;
                    opt.Password.RequiredLength = 5;
                    opt.Password.RequireUppercase = false;
                    opt.Password.RequireLowercase = false;
                    opt.Password.RequireNonAlphanumeric = false;
                })
                .AddRoles<UserRole>()
                .AddRoleManager<RoleManager<UserRole>>()
                .AddSignInManager<SignInManager<UserIdentity>>()
                .AddRoleValidator<RoleValidator<UserRole>>()
                .AddEntityFrameworkStores<IdentityContext>();
    }

    public static void AddDefaultCorsPolicy(this IServiceCollection services, CorsOptions options)
    {
        services.AddCors(opt => opt.AddPolicy(options.DefaultPolicyName, builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        }));
    }
}
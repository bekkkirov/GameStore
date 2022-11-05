using GameStore.API.Extensions;
using GameStore.Application.Options;
using GameStore.Application.Persistence;
using GameStore.Infrastructure.Persistence.Seed;

namespace GameStore.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDefaultCorsPolicy(builder.Configuration.GetSection(CorsOptions.SectionName).Get<CorsOptions>());
            builder.Services.AddControllers();
            builder.Services.AddSwaggerGen();
            builder.Services.AddGameStoreContext(builder.Configuration.GetSection(DbConnectionOptions.SectionName).Get<DbConnectionOptions>());
            builder.Services.AddRepositories();
            builder.Services.AddAutoMapper();
            builder.Services.AddFluentValidators();
            builder.Services.AddApplicationOptions(builder.Configuration);
            builder.Services.AddApplicationServices();
            builder.Services.AddJwt(builder.Configuration.GetSection(JwtOptions.SectionName).Get<JwtOptions>());
            builder.Services.AddIdentity();

            builder.AddSerilog();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseRequestMiddleware();

            app.ConfigureExceptionHandler();
            app.UseCors(builder.Configuration[$"{CorsOptions.SectionName}:DefaultPolicyName"]);

            await app.SeedDatabase();

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.UsePerformanceTrackingMiddleware();

            await app.RunAsync();
        }
    }
}
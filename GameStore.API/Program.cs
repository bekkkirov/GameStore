using GameStore.API.Extensions;
using GameStore.API.Options;
using GameStore.Application.Persistence;
using GameStore.Infrastructure.Persistence.Seed;

namespace GameStore.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddSwaggerGen();
            builder.Services.AddGameStoreContext(builder.Configuration.GetSection("ConnectionStrings").Get<DbConnectionOptions>());
            builder.Services.AddRepositories();
            builder.Services.AddAutoMapper();
            builder.Services.AddFluentValidators();
            builder.Services.AddApplicationServices();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.ConfigureExceptionHandler();
            await app.SeedDatabase();

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            await app.RunAsync();
        }
    }
}
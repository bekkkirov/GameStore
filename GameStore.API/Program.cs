using GameStore.API.Extensions;
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
            builder.Services.AddGameStoreContext(builder.Configuration);
            builder.Services.AddRepositories();
            builder.Services.AddApplicationServices();

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                await DatabaseSeeder.SeedDatabase(scope.ServiceProvider.GetRequiredService<IUnitOfWork>());
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
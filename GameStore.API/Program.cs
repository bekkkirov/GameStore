using GameStore.API.Extensions;

namespace GameStore.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            builder.Services.AddControllers();
            builder.Services.AddGameStoreContext(builder.Configuration);
            builder.Services.AddRepositories();

            var app = builder.Build();


            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
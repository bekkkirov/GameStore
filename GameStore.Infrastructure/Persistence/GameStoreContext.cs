using GameStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Infrastructure.Persistence;

public class GameStoreContext : DbContext
{
    public GameStoreContext(DbContextOptions<GameStoreContext> options) : base(options)
    {

    }

    public DbSet<Game> Games { get; set; }

    public DbSet<User> Users { get; set; }

    public DbSet<Genre> Genres { get; set; }

    public DbSet<PlatformType> Platforms { get; set; }

    public DbSet<Comment> Comments { get; set; }
}
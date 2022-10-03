using GameStore.Application.Persistence.Repositories;
using GameStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Infrastructure.Persistence.Repositories;

public class GameRepository : BaseRepository<Game>, IGameRepository
{
    public GameRepository(GameStoreContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Game>> GetWithPlatformsAndGenres()
    {
        return await _dbSet.Include(g => g.Image)
                           .Include(g => g.Genres)
                           .Include(g => g.PlatformTypes)
                           .ToListAsync();
    }

    public async Task<Game> GetByKeyAsync(string key)
    {
        return await _dbSet.Include(g => g.Image)
                           .Include(g => g.Genres)
                           .Include(g => g.PlatformTypes)
                           .FirstOrDefaultAsync(g => g.Key == key);
    } 

    public async Task<IEnumerable<Game>> GetByGenreAsync(int genreId)
    {
        return await _dbSet.Include(g => g.Image)
                           .Include(g => g.Genres)
                           .Include(g => g.PlatformTypes)
                           .Where(g => g.Genres.Any(g => g.Id == genreId))
                           .ToListAsync();
    }

    public async Task<IEnumerable<Game>> GetByPlatformAsync(int platformId)
    {
        return await _dbSet.Include(g => g.Image)
                           .Include(g => g.Genres)
                           .Include(g => g.PlatformTypes)
                           .Where(g => g.PlatformTypes.Any(pt => pt.Id == platformId))
                           .ToListAsync();
    }

    public async Task<IEnumerable<Game>> GetByNameAsync(string pattern)
    {
        return await _dbSet.Where(g => EF.Functions.Like(g.Name, $"%{pattern}%"))
                           .ToListAsync();
    }
}
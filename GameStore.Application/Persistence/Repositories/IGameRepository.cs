using GameStore.Domain.Entities;

namespace GameStore.Application.Persistence.Repositories;

public interface IGameRepository : IRepository<Game>
{
    Task<Game> GetByKeyAsync(string key);

    Task<IEnumerable<Game>> GetByGenreAsync(int genreId);

    Task<IEnumerable<Game>> GetByPlatformAsync(int platformId);
}
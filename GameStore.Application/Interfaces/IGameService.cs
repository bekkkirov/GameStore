using GameStore.Application.Models;

namespace GameStore.Application.Interfaces;

public interface IGameService
{
    Task<IEnumerable<GameModel>> GetAllAsync();

    Task<GameModel> GetByKeyAsync(string key);

    Task<IEnumerable<GameModel>> GetByGenreAsync(int genreId);

    Task<IEnumerable<GameModel>> GetByPlatformTypeAsync(int platformId);

    Task AddAsync(GameCreateModel game);

    Task UpdateAsync(string key, GameCreateModel game);

    Task DeleteAsync(int gameId);
}
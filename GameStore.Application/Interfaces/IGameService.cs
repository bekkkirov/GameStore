using GameStore.Application.Models;
using Microsoft.AspNetCore.Http;

namespace GameStore.Application.Interfaces;

public interface IGameService
{
    Task<IEnumerable<GameModel>> GetAllAsync();

    Task<GameModel> GetByKeyAsync(string key);

    Task<IEnumerable<GameModel>> GetByGenreAsync(int genreId);

    Task<IEnumerable<GameModel>> GetByPlatformTypeAsync(int platformId);

    Task<IEnumerable<GameModel>> SearchAsync(string pattern);

    Task<GameModel> AddAsync(GameCreateModel game);

    Task UpdateAsync(string key, GameCreateModel game);

    Task<ImageModel> SetImageAsync(string key, IFormFile file);

    Task DeleteAsync(int gameId);
}
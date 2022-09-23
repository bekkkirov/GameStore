using AutoMapper;
using GameStore.Application.Interfaces;
using GameStore.Application.Models;
using GameStore.Application.Persistence;
using GameStore.Domain.Entities;

namespace GameStore.Infrastructure.Services;

public class GameService : IGameService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GameService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<GameModel> AddAsync(GameCreateModel game)
    {
        var gameToAdd = _mapper.Map<Game>(game);
        await AddGenresAndPlatforms(gameToAdd, game.GenreIds, game.PlatformIds);

        _unitOfWork.GameRepository.Add(gameToAdd);
        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<GameModel>(gameToAdd);
    }

    public async Task UpdateAsync(string key, GameCreateModel game)
    {
        var gameToUpdate = await _unitOfWork.GameRepository.GetByKeyAsync(key);

        gameToUpdate.Genres.Clear();
        gameToUpdate.PlatformTypes.Clear();

        await AddGenresAndPlatforms(gameToUpdate, game.GenreIds, game.PlatformIds);

        _mapper.Map(game, gameToUpdate);

        _unitOfWork.GameRepository.Update(gameToUpdate);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task DeleteAsync(int gameId)
    {
        await _unitOfWork.GameRepository.DeleteByIdAsync(gameId);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task<GameModel> GetByKeyAsync(string key)
    {
        var game = await _unitOfWork.GameRepository.GetByKeyAsync(key);

        return _mapper.Map<GameModel>(game);
    }

    public async Task<IEnumerable<GameModel>> GetAllAsync()
    {
        var games = await _unitOfWork.GameRepository.GetWithPlatformsAndGenres();

        return _mapper.Map<IEnumerable<GameModel>>(games);
    }

    public async Task<IEnumerable<GameModel>> GetByGenreAsync(int genreId)
    {
        var games = await _unitOfWork.GameRepository.GetByGenreAsync(genreId);

        return _mapper.Map<IEnumerable<GameModel>>(games);
    }

    public async Task<IEnumerable<GameModel>> GetByPlatformTypeAsync(int platformId)
    {
        var games = await _unitOfWork.GameRepository.GetByPlatformAsync(platformId);

        return _mapper.Map<IEnumerable<GameModel>>(games);
    }

    private async Task AddGenresAndPlatforms(Game game, List<int> genreIds, List<int> platformIds)
    {
        foreach (var id in genreIds)
        {
            game.Genres.Add(await _unitOfWork.GenreRepository.GetByIdAsync(id));
        }

        foreach (var id in platformIds)
        {
            game.PlatformTypes.Add(await _unitOfWork.PlatformTypeRepository.GetByIdAsync(id));
        }
    }
}
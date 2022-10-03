using AutoMapper;
using GameStore.Application.Exceptions;
using GameStore.Application.Interfaces;
using GameStore.Application.Models;
using GameStore.Application.Persistence;
using GameStore.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace GameStore.Infrastructure.Services;

public class GameService : IGameService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IImageService _imageService;

    public GameService(IUnitOfWork unitOfWork, IMapper mapper, IImageService imageService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _imageService = imageService;
    }

    public async Task<GameModel> AddAsync(GameCreateModel game)
    {
        var gameToAdd = _mapper.Map<Game>(game);
        await AddGenresAndPlatforms(gameToAdd, game.GenreIds, game.PlatformIds);

        _unitOfWork.GameRepository.Add(gameToAdd);
        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<GameModel>(gameToAdd);
    }

    public async Task<ImageModel> SetImageAsync(string key, IFormFile file)
    {
        var game = await _unitOfWork.GameRepository.GetByKeyAsync(key);
        var image = await _imageService.AddAsync(file);

        if (game.Image is not null)
        {
            await _imageService.DeleteAsync(game.Image.PublicId);
        }

        image.GameId = game.Id;

        _unitOfWork.ImageRepository.Update(image);
        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<ImageModel>(image);
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

        if (game is null)
        {
            throw new NotFoundException("Game with specified key not found.");
        }

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

    public async Task<IEnumerable<GameModel>> GetByNameAsync(string pattern)
    {
        var games = await _unitOfWork.GameRepository.GetByNameAsync(pattern);

        return _mapper.Map<IEnumerable<GameModel>>(games);
    }

    private async Task AddGenresAndPlatforms(Game game, List<int> genreIds, List<int> platformIds)
    {
        foreach (var id in genreIds)
        {
            var genre = await _unitOfWork.GenreRepository.GetByIdAsync(id);
            game.Genres.Add(genre);
        }

        foreach (var id in platformIds)
        {
            var platform = await _unitOfWork.PlatformTypeRepository.GetByIdAsync(id);
            game.PlatformTypes.Add(platform);
        }
    }
}
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

        _unitOfWork.GameRepository.Add(gameToAdd);
        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<GameModel>(gameToAdd);
    }

    public async Task UpdateAsync(int gameId, GameCreateModel game)
    {
        var gameToUpdate = await _unitOfWork.GameRepository.GetByIdAsync(gameId);

        _mapper.Map(game, gameToUpdate);
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
        var games = await _unitOfWork.GameRepository.GetAsync();

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

    public MemoryStream DownloadGame(string key)
    {
        var memoryStream = new MemoryStream();

        using (var binaryWriter = new BinaryWriter(memoryStream))
        {
            binaryWriter.Write(key);
        }

        return memoryStream;
    }
}
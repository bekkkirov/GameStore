using AutoMapper;
using GameStore.Application.Interfaces;
using GameStore.Application.Models;
using GameStore.Application.Persistence;
using GameStore.Domain.Entities;

namespace GameStore.Infrastructure.Services;

public class CommentService : ICommentService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CommentService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<CommentModel>> GetByGameKeyAsync(string key)
    {
        var comments = await _unitOfWork.CommentRepository.GetByGameKeyAsync(key);

        return _mapper.Map<IEnumerable<CommentModel>>(comments);
    }

    public async Task<CommentModel> AddAsync(string userName, string gameKey, CommentCreateModel comment)
    {
        var user = await _unitOfWork.UserRepository.GetByUserNameAsync(userName);
        var game = await _unitOfWork.GameRepository.GetByKeyAsync(gameKey);

        var commentToAdd = _mapper.Map<Comment>(comment);
        commentToAdd.AuthorId = user.Id;
        commentToAdd.GameId = game.Id;

        _unitOfWork.CommentRepository.Add(commentToAdd);
        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<CommentModel>(commentToAdd);
    }
}
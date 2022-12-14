using AutoMapper;
using GameStore.Application.Exceptions;
using GameStore.Application.Interfaces;
using GameStore.Application.Models;
using GameStore.Application.Persistence;
using GameStore.Domain.Entities;

namespace GameStore.Infrastructure.Services;

public class CommentService : ICommentService
{
    private readonly ICurrentUserService _currentUserService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CommentService(IUnitOfWork unitOfWork, IMapper mapper, ICurrentUserService currentUserService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _currentUserService = currentUserService;
    }

    public async Task<IEnumerable<CommentModel>> GetByGameKeyAsync(string key)
    {
        var comments = await _unitOfWork.CommentRepository.GetByGameKeyAsync(key);

        return _mapper.Map<IEnumerable<CommentModel>>(comments);
    }

    public async Task<CommentModel> AddAsync(string gameKey, CommentCreateModel comment)
    {
        var userName = _currentUserService.GetUsername();
        var user = await _unitOfWork.UserRepository.GetByUserNameAsync(userName);
        var game = await _unitOfWork.GameRepository.GetByKeyAsync(gameKey);

        var commentToAdd = _mapper.Map<Comment>(comment);

        if (commentToAdd.ParentCommentId is null)
        {
            commentToAdd.IsRoot = true;
        }

        commentToAdd.AuthorId = user.Id;
        commentToAdd.GameId = game.Id;

        _unitOfWork.CommentRepository.Add(commentToAdd);
        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<CommentModel>(commentToAdd);
    }

    public async Task UpdateAsync(int commentId, CommentCreateModel updateData)
    {
        var commentToUpdate = await _unitOfWork.CommentRepository.GetByIdWithAuthorAsync(commentId);

        CheckCommentAuthor(commentToUpdate);

        _mapper.Map(updateData, commentToUpdate);

        _unitOfWork.CommentRepository.Update(commentToUpdate);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task MarkForDeletionAsync(int commentId)
    {
        var comment = await _unitOfWork.CommentRepository.GetByIdWithAuthorAsync(commentId);

        CheckCommentAuthor(comment);

        comment.IsMarkedForDeletion = true;
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task DeleteMarkedCommentAsync(string gameKey)
    {
        var username = _currentUserService.GetUsername();

        _unitOfWork.CommentRepository.RemoveMarkedCommentsAsync(username, gameKey);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task RestoreAsync(int commentId)
    {
        var comment = await _unitOfWork.CommentRepository.GetByIdAsync(commentId);

        comment.IsMarkedForDeletion = false;
        await _unitOfWork.SaveChangesAsync();
    }

    private void CheckCommentAuthor(Comment comment)
    {
        var currentUser = _currentUserService.GetUsername();

        if (comment.Author.UserName != currentUser)
        {
            throw new NotAllowedException("You aren't allowed to perform this action.");
        }
    }
}
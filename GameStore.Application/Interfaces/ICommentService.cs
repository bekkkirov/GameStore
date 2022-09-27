using GameStore.Application.Models;

namespace GameStore.Application.Interfaces;

public interface ICommentService
{
    Task<IEnumerable<CommentModel>> GetByGameKeyAsync(string key);

    Task<CommentModel> AddAsync(string userName, string gameKey, CommentCreateModel comment);
}
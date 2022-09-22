using GameStore.Application.Models;

namespace GameStore.Application.Interfaces;

public interface ICommentService
{
    Task<IEnumerable<CommentModel>> GetByGameKeyAsync(string key);

    Task AddAsync(string userName, CommentCreateModel comment);
}
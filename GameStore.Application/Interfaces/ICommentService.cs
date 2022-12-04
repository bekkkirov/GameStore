using GameStore.Application.Models;

namespace GameStore.Application.Interfaces;

public interface ICommentService
{
    Task<IEnumerable<CommentModel>> GetByGameKeyAsync(string key);

    Task<CommentModel> AddAsync(string gameKey, CommentCreateModel comment);

    Task UpdateAsync(int commentId, CommentCreateModel updateData);

    Task MarkForDeletionAsync(int commentId);

    Task DeleteMarkedCommentAsync(string gameKey);

    Task RestoreAsync(int commentId);
}
using GameStore.Application.Models;

namespace GameStore.Application.Interfaces;

public interface ICommentService
{
    Task<IEnumerable<CommentModel>> GetByGameKeyAsync(string key);

    Task<CommentModel> AddAsync(string userName, string gameKey, CommentCreateModel comment);

    Task UpdateAsync(string requesterUserName, int commentId, CommentCreateModel updateData);

    Task MarkForDeletionAsync(string requesterUserName, int commentId);

    Task DeleteMarkedCommentAsync(string userName, string gameKey);

    Task RestoreAsync(int commentId);
}
using GameStore.Domain.Entities;

namespace GameStore.Application.Persistence.Repositories;

public interface ICommentRepository : IRepository<Comment>
{
    Task<IEnumerable<Comment>> GetByGameKeyAsync(string key);

    void RemoveMarkedCommentsAsync(string userName, string key);

    Task<Comment> GetByIdWithAuthorAsync(int id);
}
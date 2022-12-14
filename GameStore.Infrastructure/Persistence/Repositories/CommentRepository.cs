using GameStore.Application.Persistence.Repositories;
using GameStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Infrastructure.Persistence.Repositories;

public class CommentRepository : BaseRepository<Comment>, ICommentRepository
{
    public CommentRepository(GameStoreContext context) : base(context)
    {
    }

    public async Task<Comment> GetByIdWithAuthorAsync(int id)
    {
        return await _dbSet.Include(c => c.Author)
                           .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<IEnumerable<Comment>> GetByGameKeyAsync(string key)
    {
        return await _dbSet.Where(c => c.Game.Key == key && c.IsRoot)
                           .Include(c => c.Author)
                           .Include(c => c.Replies)
                           .ToListAsync();
    }

    public void RemoveMarkedCommentsAsync(string userName, string key)
    {
        var commentsToDelete = _dbSet.Include(c => c.Replies)
                                                      .Where(c => c.Author.UserName == userName &&
                                                       c.Game.Key == key &&
                                                       c.IsMarkedForDeletion);

        _dbSet.RemoveRange(commentsToDelete);
    }
}
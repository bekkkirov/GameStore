using GameStore.Application.Persistence.Repositories;
using GameStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Infrastructure.Persistence.Repositories;

public class CommentRepository : BaseRepository<Comment>, ICommentRepository
{
    public CommentRepository(GameStoreContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Comment>> GetByGameKeyAsync(string key)
    {
        return await _dbSet.Where(c => c.Game.Key == key).ToListAsync();
    }
}
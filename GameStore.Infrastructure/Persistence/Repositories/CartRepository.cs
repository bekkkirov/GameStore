using GameStore.Application.Persistence.Repositories;
using GameStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Infrastructure.Persistence.Repositories;

public class CartRepository : BaseRepository<Cart>, ICartRepository
{
    public CartRepository(GameStoreContext context) : base(context)
    {
    }

    public async Task<Cart> GetCurrentCartAsync(string userName)
    {
        return await _dbSet.Include(c => c.User)
                           .Include(c => c.Items)
                           .ThenInclude(i => i.Game)
                           .SingleOrDefaultAsync(c => c.User.UserName == userName && !c.IsOrdered);
    }
}
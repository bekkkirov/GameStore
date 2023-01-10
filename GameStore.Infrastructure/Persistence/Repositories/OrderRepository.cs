using GameStore.Application.Persistence.Repositories;
using GameStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Infrastructure.Persistence.Repositories;

public class OrderRepository : BaseRepository<Order>, IOrderRepository
{
    public OrderRepository(GameStoreContext context) : base(context)
    {
    }

    public async Task<Order> GetByIdWithItemsAsync(int id)
    {
        return await _dbSet.Include(o => o.Items)
                           .FirstOrDefaultAsync(o => o.Id == id);
    }
}
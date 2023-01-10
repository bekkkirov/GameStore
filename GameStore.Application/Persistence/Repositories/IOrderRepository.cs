using GameStore.Domain.Entities;

namespace GameStore.Application.Persistence.Repositories;

public interface IOrderRepository : IRepository<Order>
{
    Task<Order> GetByIdWithItemsAsync(int id);
}
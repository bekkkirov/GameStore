using GameStore.Domain.Entities;

namespace GameStore.Application.Persistence.Repositories;

public interface ICartRepository : IRepository<Cart>
{
    Task<Cart> GetCurrentCartAsync(string userName);
}
using GameStore.Domain.Entities;

namespace GameStore.Application.Persistence.Repositories;

public interface IUserRepository : IRepository<User>
{
    Task<User> GetByUserNameAsync(string userName);
}
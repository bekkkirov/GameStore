using GameStore.Application.Persistence.Repositories;
using GameStore.Domain.Entities;

namespace GameStore.Infrastructure.Persistence.Repositories;

public class UserRepository : BaseRepository<User>, IUserRepository
{
    public UserRepository(GameStoreContext context) : base(context)
    {
    }
}
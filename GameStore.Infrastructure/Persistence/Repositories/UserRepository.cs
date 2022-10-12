using GameStore.Application.Persistence.Repositories;
using GameStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Infrastructure.Persistence.Repositories;

public class UserRepository : BaseRepository<User>, IUserRepository
{
    public UserRepository(GameStoreContext context) : base(context)
    {
    }

    public async Task<User> GetByUserNameAsync(string userName)
    {
        return await _dbSet.Include(u => u.ProfileImage)
                           .FirstOrDefaultAsync(u => u.UserName == userName);
    }
}
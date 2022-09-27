using GameStore.Application.Persistence.Repositories;
using GameStore.Domain.Entities;

namespace GameStore.Infrastructure.Persistence.Repositories;

public class PlatformTypeRepository : BaseRepository<PlatformType>, IPlatformTypeRepository
{
    public PlatformTypeRepository(GameStoreContext context) : base(context)
    {
    }
}
using GameStore.Application.Persistence.Repositories;
using GameStore.Domain.Entities;

namespace GameStore.Infrastructure.Persistence.Repositories;

public class ImageRepository : BaseRepository<Image>, IImageRepository
{
    public ImageRepository(GameStoreContext context) : base(context)
    {
    }
}
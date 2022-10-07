using GameStore.Application.Exceptions;
using GameStore.Application.Persistence.Repositories;
using GameStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Infrastructure.Persistence.Repositories;

public class ImageRepository : BaseRepository<Image>, IImageRepository
{
    public ImageRepository(GameStoreContext context) : base(context)
    {
    }

    public async Task<Image> GetByPublicIdAsync(string publicId)
    {
        return await _dbSet.FirstOrDefaultAsync(i => i.PublicId == publicId);
    }

    public async Task DeleteByPublicIdAsync(string publicId)
    {
        var image = await _dbSet.FirstOrDefaultAsync(i => i.PublicId == publicId);

        if (image is null)
        {
            throw new NotFoundException("Image with specified public id not found.");
        }

        _dbSet.Remove(image);
    }
}
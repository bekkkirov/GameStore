using GameStore.Domain.Entities;

namespace GameStore.Application.Persistence.Repositories;

public interface IImageRepository : IRepository<Image>
{
    Task<Image> GetByPublicIdAsync(string publicId);

    Task DeleteByPublicIdAsync(string publicId);
}
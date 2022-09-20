using GameStore.Application.Persistence.Repositories;
using GameStore.Domain.Entities;

namespace GameStore.Infrastructure.Persistence.Repositories;

public class GenreRepository : BaseRepository<Genre>, IGenreRepository
{
    public GenreRepository(GameStoreContext context) : base(context)
    {
    }
}
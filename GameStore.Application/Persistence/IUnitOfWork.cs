using GameStore.Application.Persistence.Repositories;

namespace GameStore.Application.Persistence;

public interface IUnitOfWork
{
    public IUserRepository UserRepository { get; }

    public IGameRepository GameRepository { get; }

    public ICommentRepository CommentRepository { get; }

    public IGenreRepository GenreRepository { get; }

    public IPlatformTypeRepository PlatformTypeRepository { get; }

    public IImageRepository ImageRepository { get; }

    Task SaveChangesAsync();
}
using GameStore.Application.Persistence;
using GameStore.Application.Persistence.Repositories;

namespace GameStore.Infrastructure.Persistence;

public class UnitOfWork : IUnitOfWork
{
    private readonly GameStoreContext _context;

    public IUserRepository UserRepository { get; }

    public IGameRepository GameRepository { get; }

    public ICommentRepository CommentRepository { get; }

    public IGenreRepository GenreRepository { get; }

    public IPlatformTypeRepository PlatformTypeRepository { get; }

    public IImageRepository ImageRepository {get;}

    public ICartRepository CartRepository { get; }

    public IOrderRepository OrderRepository { get; }

    public UnitOfWork(IUserRepository userRepository,
                      IGameRepository gameRepository,
                      ICommentRepository commentRepository,
                      IGenreRepository genreRepository,
                      IPlatformTypeRepository platformTypeRepository,
                      IImageRepository imageRepository,
                      ICartRepository cartRepository,
                      IOrderRepository orderRepository,
                      GameStoreContext context)
    {
        UserRepository = userRepository;
        GameRepository = gameRepository;
        CommentRepository = commentRepository;
        GenreRepository = genreRepository;
        PlatformTypeRepository = platformTypeRepository;
        ImageRepository = imageRepository;
        CartRepository = cartRepository;
        OrderRepository = orderRepository;

        _context = context;
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
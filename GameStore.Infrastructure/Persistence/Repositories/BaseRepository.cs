using System.Linq.Expressions;
using GameStore.Application.Persistence.Repositories;
using GameStore.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Infrastructure.Persistence.Repositories;

public abstract class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
{
    protected readonly GameStoreContext _context;
    protected readonly DbSet<TEntity> _dbSet;

    protected BaseRepository(GameStoreContext context)
    {
        _context = context;
        _dbSet = _context.Set<TEntity>();
    }

    public async Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> filter = null)
    {
        if (filter is null)
        {
            return await _dbSet.ToListAsync();
        }

        return await _dbSet.Where(filter)
                           .ToListAsync();
    }

    public async Task<TEntity> GetByIdAsync(int id)
    {
        return await _dbSet.FindAsync(id);
    }

    public void Add(TEntity entity)
    {
        _dbSet.Add(entity);
    }

    public void Update(TEntity entity)
    {
        _dbSet.Update(entity);
    }

    public async Task DeleteByIdAsync(int id)
    {
        var entityToDelete = await _dbSet.FindAsync(id);

        _dbSet.Remove(entityToDelete);
    }
}
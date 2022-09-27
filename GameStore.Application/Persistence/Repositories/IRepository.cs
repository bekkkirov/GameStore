using System.Linq.Expressions;
using GameStore.Domain.Common;

namespace GameStore.Application.Persistence.Repositories;

public interface IRepository<TEntity> where TEntity : BaseEntity
{
    Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> filter = null);

    Task<TEntity> GetByIdAsync(int id);

    void Add(TEntity entity);

    void Update(TEntity entity);

    Task DeleteByIdAsync(int id);
}
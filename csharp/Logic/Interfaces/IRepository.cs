using Todo.Logic.DomainObjects.Entities;

namespace Todo.Logic.Interfaces;
public interface IRepository<TEntity> where TEntity : BaseEntity
{
    Task<TEntity> GetAsync(string partitionKey, string rowKey);
    Task<IEnumerable<TEntity>> GetAsync(string partitionKey);
    Task<IEnumerable<TEntity>> GetByQueryAsync(Func<TEntity, bool> predicate, string partitionKey);
    Task<TEntity> GetFirstByQueryAsync(Func<TEntity, bool> predicate, string partitionKey);

    Task AddAsync(TEntity entity);
    Task UpdateAsync(TEntity entity);
    Task AddOrUpdateAsync(TEntity entity);
    Task DeleteAsync(TEntity entity);
}
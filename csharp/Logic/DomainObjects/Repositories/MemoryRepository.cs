using Todo.Logic.DomainObjects.Entities;
using Todo.Logic.Interfaces;

namespace Todo.Logic.DomainObjects.Repositories;
public class MemoryRepository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
{
    private readonly IDictionary<string, IDictionary<string, TEntity>> _entities;

    public MemoryRepository()
    {
        _entities = new Dictionary<string, IDictionary<string, TEntity>>();
    }

    public async Task AddAsync(TEntity entity)
    {
       if(_entities.ContainsKey(entity.PartitionKey))
       {
            if(_entities[entity.PartitionKey].ContainsKey(entity.RowKey))
            {
                throw new Exception("Entity already exists");
            }
            _entities[entity.PartitionKey][entity.RowKey] = entity;
            return;
       }
         _entities.Add(entity.PartitionKey, new Dictionary<string, TEntity> { { entity.RowKey, entity } });
    }

    public async Task AddOrUpdateAsync(TEntity entity)
    {
        if (_entities.ContainsKey(entity.PartitionKey) &&
            _entities[entity.PartitionKey].ContainsKey(entity.RowKey))
        {
            entity.SetUpdatedAt();
            _entities[entity.PartitionKey][entity.RowKey] = entity;
        }

        _entities.Add(entity.PartitionKey, new Dictionary<string, TEntity> { { entity.RowKey, entity } });
    }


    public async Task UpdateAsync(TEntity entity)
    {
        if (!_entities.ContainsKey(entity.PartitionKey) || !_entities[entity.PartitionKey].ContainsKey(entity.RowKey))
        {
            throw new Exception("Entity does not exists");
        }
        entity.SetUpdatedAt();
        _entities[entity.PartitionKey][entity.RowKey] = entity;
    }

    public async Task DeleteAsync(TEntity entity)
    {
        if (!_entities.ContainsKey(entity.PartitionKey) || !_entities[entity.PartitionKey].ContainsKey(entity.RowKey))
        {
            return;
        }

        _entities[entity.PartitionKey].Remove(entity.RowKey);

        if (!_entities[entity.PartitionKey].Any())
        {
            _entities.Remove(entity.PartitionKey);
        }
    }

    public async Task<TEntity> GetAsync(string partitionKey, string rowKey)
    {
        if (_entities.ContainsKey(partitionKey) &&
            _entities[partitionKey].ContainsKey(rowKey))
        {
            return _entities[partitionKey][rowKey];
        }
        return null;
    }

    public async Task<IEnumerable<TEntity>> GetAsync(string partitionKey)
    {
        if (_entities.ContainsKey(partitionKey))
        {
            return _entities[partitionKey].Values.AsEnumerable();
        }
        return new List<TEntity>();
    }

    public async Task<IEnumerable<TEntity>> GetByQueryAsync(Func<TEntity, bool> predicate, string partitionKey)
    {
        if (_entities.ContainsKey(partitionKey))
        {
           return _entities[partitionKey].Values.Where(predicate)
                                                .Cast<TEntity>();
        }
        return new List<TEntity>();
    }

    public async Task<TEntity> GetFirstByQueryAsync(Func<TEntity, bool> predicate, string partitionKey)
    {
        if (_entities.ContainsKey(partitionKey))
        {
            return _entities[partitionKey].Values.FirstOrDefault(predicate);
        }
        return null;
    }
}
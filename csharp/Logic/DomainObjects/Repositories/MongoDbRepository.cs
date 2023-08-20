using System.Reflection;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using Todo.Logic.DomainObjects.Entities;
using Todo.Logic.Interfaces;

namespace Todo.Logic.DomainObjects.Repositories;

public class MongoDbRepository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity, new()
{
    private readonly ILogger<MongoDbRepository<TEntity>> _logger;
    private readonly IConfiguration _configuration;
    private readonly MongoClient _client;
    private readonly string _databaseName;
    private readonly string _collectionName;

    public MongoDbRepository(
        ILogger<MongoDbRepository<TEntity>> logger,
        IConfiguration configuration)
    {
        _logger = logger;
        _configuration = configuration;

        _databaseName = _configuration.GetValue<string>("Database:MongoDB:DatabaseName");
        _collectionName = _configuration.GetValue<string>($"Database:TableNames:{typeof(TEntity).Name}");

        if (string.IsNullOrEmpty(_collectionName))
        {
            throw new Exception($"Table name for {typeof(TEntity).Name} not defined in Appsettings");
        }

        var username = _configuration.GetValue<string>("Database:MongoDB:Username");
        var password = _configuration.GetValue<string>("Database:MongoDB:Password");
        var connectionString = _configuration.GetValue<string>("Database:MongoDB:ConnectionString")
                                             .Replace("{username}", username)
                                             .Replace("{password}", password);

        _client = new MongoClient(connectionString);
    }


    public async Task AddAsync(TEntity entity)
    {
        BsonDocument bsonEntity = ToBsonDocument(entity);
        await GetCollection().InsertOneAsync(bsonEntity);
    }

    public async Task AddOrUpdateAsync(TEntity entity)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteAsync(TEntity entity)
    {
        throw new NotImplementedException();
    }

    public async Task<TEntity> GetAsync(string partitionKey, string rowKey)
    {
        var collection = GetCollection();
        var filter = Builders<BsonDocument>.Filter.Eq("PartitionKey", partitionKey) &
                     Builders<BsonDocument>.Filter.Eq("RowKey", rowKey);

        BsonDocument result = (await collection.FindAsync(filter))
                                               .FirstOrDefault();

        return ToEntity(result);
    }

    public async Task<IEnumerable<TEntity>> GetAsync(string partitionKey)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<TEntity>> GetByQueryAsync(Func<TEntity, bool> predicate, string partitionKey)
    {
        throw new NotImplementedException();
    }

    public async Task<TEntity> GetFirstByQueryAsync(Func<TEntity, bool> predicate, string partitionKey)
    {
        throw new NotImplementedException();
    }

    public async Task UpdateAsync(TEntity entity)
    {
        throw new NotImplementedException();
    }

    private IMongoCollection<BsonDocument> GetCollection()
    {
        return _client.GetDatabase(_databaseName).GetCollection<BsonDocument>(_collectionName);
    }

    private BsonDocument ToBsonDocument(TEntity entity)
    {
        var document = new BsonDocument();
        var properties = GetType().GetProperties(BindingFlags.Public);
        foreach (var property in properties)
        {
            var value = property.GetValue(this);
            document.Add(property.Name, BsonValue.Create(value));
        }
        return document;
    }

    private TEntity ToEntity(BsonDocument document)
    {
        var entity = new TEntity();
        var properties = typeof(TEntity).GetProperties(BindingFlags.Public | BindingFlags.Instance);
        foreach (var property in properties)
        {
            var attribute = property.GetCustomAttribute<BsonElementAttribute>();
            var name = attribute?.ElementName ?? property.Name;
            if (document.Equals(name))
            {
                var value = document[name];
                if (!value.IsBsonNull)
                {
                    property.SetValue(entity, BsonTypeMapper.MapToDotNetValue(value));
                }
            }
        }
        return entity;
    }
}
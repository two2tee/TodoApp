namespace Todo.Logic.DomainObjects.Entities;

public abstract class BaseEntity
{
    public string PartitionKey { get; set; }
    public string RowKey { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public BaseEntity(string partitionKey = null, string rowKey = null)
    {
        PartitionKey = string.IsNullOrEmpty(partitionKey) ? "" : rowKey;
        RowKey = string.IsNullOrEmpty(RowKey) ? Guid.NewGuid().ToString() : RowKey;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }


    public void SetUpdatedAt()
    {
        UpdatedAt = DateTime.UtcNow;
    }
}
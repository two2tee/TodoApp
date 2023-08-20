namespace Todo.Logic.DomainObjects.Entities;
public class TodoEntity : BaseEntity
{

    public TodoEntity(string userId) : base()
    {
        PartitionKey = userId;
        UserId = PartitionKey;
        TodoId = RowKey;
    }

    public string TodoId { get; set; }

    public string UserId { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public DateTime? CompletedAt { get; set; }

    public DateTime? DueDate { get; set; }
}
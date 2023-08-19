namespace Todo.Logic.DomainObjects.Entities;
public class TodoEntity : BaseEntity
{

    public TodoEntity(string userId) : base()
    {
        PartitionKey = userId;
    }

    public string UserId { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public DateTime? CompletedAt { get; set; }

    public DateTime? DueDate { get; set; }
}
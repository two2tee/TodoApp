namespace Todo.Logic.DomainObjects.Entities;
public class UserEntity : BaseEntity
{

    public UserEntity() : base()
    {
        UserId = RowKey;
    }

    public string UserId { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Email { get; set; }

}
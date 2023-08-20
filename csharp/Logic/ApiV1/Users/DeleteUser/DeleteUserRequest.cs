namespace Todo.Logic.ApiV1.Users;

public class DeleteUserRequest
{
    [FromQuery(Name = "userId")]
    public string UserId { get; set; }
}
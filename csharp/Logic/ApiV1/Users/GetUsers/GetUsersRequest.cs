namespace Todo.Logic.ApiV1.Users;

public class GetUsersRequest
{
    [FromQuery(Name = "email")]
    public string Email { get; set; }
}
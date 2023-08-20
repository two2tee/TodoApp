namespace Todo.Logic.ApiV1.Users;

public class DeleteUserRequest
{
    [JsonPropertyName("userId")]
    public string UserId { get; set; }
}
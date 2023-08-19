namespace Todo.Logic.ApiV1.Users;

public class UserDto
{
    [JsonPropertyName("userId")]
    public string UserId { get; set; }

    [JsonPropertyName("firstName")]
    public string FirstName { get; set; }

    [JsonPropertyName("lastName")]
    public string LastName { get; set; }

    [JsonPropertyName("email")]
    public string Email { get; set; }
}
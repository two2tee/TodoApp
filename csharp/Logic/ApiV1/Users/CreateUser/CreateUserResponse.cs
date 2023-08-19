namespace Todo.Logic.ApiV1.Users;

public class CreateUserResponse
{
    [JsonPropertyName("isSuccess")]
    public bool IsSuccess { get; set; }

    [JsonPropertyName("errorMessage")]
    public string ErrorMessage { get; set; }
}
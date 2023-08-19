namespace Todo.Logic.ApiV1.Users;

public class GetUsersResponse
{
    [JsonPropertyName("users")]
    public IList<UserDto> Users { get; set; } = new List<UserDto>();

    [JsonPropertyName("isSuccess")]
    public bool IsSuccess { get; set; }

    [JsonPropertyName("errorMessage")]
    public string ErrorMessage { get; set; }
}
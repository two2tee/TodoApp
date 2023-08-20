namespace Todo.Logic.ApiV1.Users;

public class GetUsersResponse : ApiV1BaseResponse
{
    [JsonPropertyName("users")]
    public IList<UserDto> Users { get; set; } = new List<UserDto>();
}
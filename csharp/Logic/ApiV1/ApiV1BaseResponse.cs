
namespace Todo.Logic.ApiV1;

public abstract class ApiV1BaseResponse
{
    [JsonPropertyName("isSuccess")]
    public bool IsSuccess { get; set; }

    [JsonPropertyName("errorMessage")]
    public string ErrorMessage { get; set; }

    [JsonPropertyName("errorReason")]
    public string ErrorReason { get; set; }
}
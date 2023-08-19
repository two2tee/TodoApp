namespace Todo.Logic.ApiV1.Status;

public class GetStatusResponse
{
    [JsonPropertyName("version")]
    public string Version { get; set; }

    [JsonPropertyName("environment")]
    public string Environment { get; set; }
}

using System.ComponentModel.DataAnnotations;

namespace Todo.Logic.ApiV1.Users;

public class CreateUserRequest
{
    [JsonPropertyName("firstName")]
    public string FirstName { get; set; }

    [JsonPropertyName("lastName")]
    public string LastName { get; set; }

    [JsonPropertyName("email")]
    [Required]
    public string Email { get; set; }
}
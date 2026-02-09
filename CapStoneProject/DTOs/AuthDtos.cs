using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CapStoneProject.DTOs
{
    public class RegisterDto
    {
        [Required]
        [MaxLength(255)]
        [JsonPropertyName("name")]
        public string Name { get; set; } = null!;

        [Required]
        [EmailAddress]
        [JsonPropertyName("email")]
        public string Email { get; set; } = null!;

        [Required]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters")]
        [JsonPropertyName("password")]
        public string Password { get; set; } = null!;
    }

    public class LoginDto
    {
        [Required]
        [EmailAddress]
        [JsonPropertyName("email")]
        public string Email { get; set; } = null!;

        [Required]
        [JsonPropertyName("password")]
        public string Password { get; set; } = null!;
    }

    public class AuthResponseDto
    {
        [JsonPropertyName("token")]
        public string Token { get; set; } = null!;

        [JsonPropertyName("user")]
        public UserDto User { get; set; } = null!;
    }

    public class UserDto
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; } = null!;

        [JsonPropertyName("email")]
        public string Email { get; set; } = null!;

        [JsonPropertyName("role")]
        public string Role { get; set; } = null!;
    }
}

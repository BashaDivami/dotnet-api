using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CapStoneProject.DTOs
{
    public class CreatePolicyDto
    {
        [Required]
        [MaxLength(255)]
        [JsonPropertyName("name")]
        public string Name { get; set; } = null!;

        [Required]
        [Range(1, 1000000, ErrorMessage = "Premium amount must be greater than 0")]
        [JsonPropertyName("premium_amount")]
        public decimal PremiumAmount { get; set; }

        [Required]
        [JsonPropertyName("description")]
        public string Description { get; set; } = null!;

        [JsonPropertyName("is_active")]
        public bool IsActive { get; set; } = true;
    }
}

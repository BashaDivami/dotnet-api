using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CapStoneProject.DTOs
{
    public class EnrollmentRequestDto
    {
        [JsonPropertyName("policy_id")]
        public int PolicyId { get; set; }
    }

    public class EnrollmentResponseDto
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("user_id")]
        public int UserId { get; set; }

        [JsonPropertyName("policy_id")]
        public int PolicyId { get; set; }

        [JsonPropertyName("policy_name")]
        public string PolicyName { get; set; } = null!;

        [JsonPropertyName("status")]
        public string Status { get; set; } = null!;

        [JsonPropertyName("requested_at")]
        public DateTime RequestedAt { get; set; }

        [JsonPropertyName("approved_at")]
        public DateTime? ApprovedAt { get; set; }
    }

    public class ApprovalDto
    {
        [JsonPropertyName("rejection_reason")]
        public string? RejectionReason { get; set; }
    }
}

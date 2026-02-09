using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CapStoneProject.Entities
{
    [Table("policy_enrollments")]
    public class PolicyEnrollment
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("user_id")]
        public int UserId { get; set; }

        [Required]
        [Column("policy_id")]
        public int PolicyId { get; set; }

        [Required]
        [MaxLength(50)]
        [Column("status")]
        public string Status { get; set; } = "Pending"; // Pending, Approved, Rejected

        [Column("requested_at")]
        public DateTime RequestedAt { get; set; } = DateTime.UtcNow;

        [Column("approved_at")]
        public DateTime? ApprovedAt { get; set; }

        [Column("approved_by")]
        public int? ApprovedBy { get; set; }

        [Column("rejection_reason")]
        public string? RejectionReason { get; set; }

        // Navigation properties
        [ForeignKey("UserId")]
        public User User { get; set; } = null!;

        [ForeignKey("PolicyId")]
        public Policy Policy { get; set; } = null!;
    }
}

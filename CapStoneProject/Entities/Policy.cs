using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CapStoneProject.Entities
{
    [Table("policies")]
    public class Policy
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        [Column("name")]
        public string Name { get; set; } = null!;
        [Required]
        [Range(0, 1000000, ErrorMessage = "Premium amount must be a positive value.")]
        [Column("premium_amount")]
        public decimal PremiumAmount { get; set; }

        [Column("description")]
        public string Description { get; set; } = null!;
        
        [Column("is_active")]
        public bool IsActive { get; set; } = true;

        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}

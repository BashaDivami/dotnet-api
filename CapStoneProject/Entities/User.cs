using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CapStoneProject.Entities
{
    [Table("users")]
    public class User
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        [Column("name")]
        public string Name { get; set; } = null!;
        
        [Column("email")]
        public string Email { get; set; } = null!;

        [Column("password")]
        public string Password { get; set; } = null!;
        
        [Column("role")]
        public string Role { get; set; } = null!;

        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}

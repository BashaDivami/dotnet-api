using CapStoneProject.Entities;
using Microsoft.EntityFrameworkCore;
namespace CapStoneProject.Data
{
    public class AppDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Policy> Policies { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<PolicyEnrollment> PolicyEnrollments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure unique constraint: user can enroll in same policy only once
            modelBuilder.Entity<PolicyEnrollment>()
                .HasIndex(pe => new { pe.UserId, pe.PolicyId })
                .IsUnique();
        }
    }
}

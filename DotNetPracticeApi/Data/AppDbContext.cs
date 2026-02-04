using Microsoft.EntityFrameworkCore;
using DotNetPracticeApi.Entities;

namespace DotNetPracticeApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public DbSet<User> Users => Set<User>();
    }
}

using DotNetPracticeApi.Data;
using DotNetPracticeApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace DotNetPracticeApi.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext DbConnection;

        public UserRepository(AppDbContext context)
        {
            DbConnection = context;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await DbConnection.Users.ToListAsync();
        }

        public async Task<User?> GetByIdAsync(Guid id)
        {
            return await DbConnection.Users.FindAsync(id);
        }

        public async Task<User> CreateAsync(User user)
        {
            DbConnection.Users.Add(user);
            await DbConnection.SaveChangesAsync();
            return user;
        }
        public async Task<bool> DeleteByName(string name)
        {
            var user = await DbConnection.Users
                .FirstOrDefaultAsync(u => u.Name == name);

            if (user == null)
                return false;

            DbConnection.Users.Remove(user);
            await DbConnection.SaveChangesAsync();
            return true;
        }

    }
}

using CapStoneProject.Entities;
using CapStoneProject.Data;
using Microsoft.EntityFrameworkCore;

namespace CapStoneProject.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext dbContext;

        public UserRepository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

       
        public async Task<User> CreateUser(User user)
        {
            dbContext.Users.Add(user);
            await dbContext.SaveChangesAsync();
            return user;
        }
    }
}

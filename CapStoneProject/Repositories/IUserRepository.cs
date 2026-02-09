using CapStoneProject.Entities;

namespace CapStoneProject.Repositories
{
    public interface IUserRepository
    {
        // Task<IEnumerable<User>> GetUsers(); 
        // Task<User?> GetUserById(int id);
        // Task<IEnumerable<User>> SearchUsersByName(string name);
        // Task<IEnumerable<User>> GetUsersByStatus(bool isActive);

        Task<User> CreateUser(User user);
        

    }
}

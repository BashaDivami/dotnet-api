using CapStoneProject.Entities;
namespace CapStoneProject.Services
{
    public interface IUserService
    {
        // Task<User?> GetUserById(int id);
        // Task<User?> GetUserByEmail(string email);
        Task<User> CreateUser(User user);
        // Task<bool> ValidateUserCredentials(string email, string password);
    }
}
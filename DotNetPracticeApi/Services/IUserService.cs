using DotNetPracticeApi.Entities;

namespace DotNetPracticeApi.Services
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetUsers();
        Task<User?> GetUserById(Guid id);
        Task<User> CreateUser(User user);
        Task<bool> DeleteUserByName(string name);

    }
}

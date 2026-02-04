using DotNetPracticeApi.Entities;

namespace DotNetPracticeApi.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllAsync();
        Task<User?> GetByIdAsync(Guid id);
        Task<User> CreateAsync(User user);
        Task<bool> DeleteByName(string name);

    }
}

using DotNetPracticeApi.Entities;
using DotNetPracticeApi.Repositories;

namespace DotNetPracticeApi.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;

        public UserService(IUserRepository repository)
        {
            userRepository = repository;
        }

        public Task<IEnumerable<User>> GetUsers()
        {
            return userRepository.GetAllAsync();
        }

        public Task<User?> GetUserById(Guid id)
        {
            return userRepository.GetByIdAsync(id);
        }

        public Task<User> CreateUser(User user)
        {
            return userRepository.CreateAsync(user);
        }

        public async Task<bool> DeleteUserByName(string name)
        {
            return await userRepository.DeleteByName(name);
        }

    }
}

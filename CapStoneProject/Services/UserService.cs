using CapStoneProject.Entities;
using CapStoneProject.Repositories;
namespace CapStoneProject.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;

        public UserService(IUserRepository repository)
        {
            userRepository = repository;
        }

        public async Task<User> CreateUser(User user)
        {
            return await userRepository.CreateUser(user);
        }
    }
}
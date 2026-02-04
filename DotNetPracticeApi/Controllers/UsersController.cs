using Microsoft.AspNetCore.Mvc;
using DotNetPracticeApi.Entities;
using DotNetPracticeApi.Services;

namespace DotNetPracticeApi.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService userService;

        public UsersController(IUserService service)
        {
            userService = service;
        }

        // get users
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            return Ok(await userService.GetUsers());
        }
        
        //  get user by id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(Guid id)
        {
            var user = await userService.GetUserById(id);
            if (user == null)
                return NotFound();

            return Ok(user);
        }

        // create user
        [HttpPost("create")]
        public async Task<IActionResult> CreateUser([FromBody] User user)
        {
            var createdUser = await userService.CreateUser(user);
            return Ok(createdUser);
        }

        //  delete user by name
        [HttpDelete("by-name/{name}")]
        public async Task<IActionResult> DeleteUserByName(string name)
        {
            var deleted = await userService.DeleteUserByName(name);

            if (!deleted)
                return NotFound($"User with name '{name}' not found");

            return Ok($"User '{name}' deleted successfully");
        }

        
    }
}

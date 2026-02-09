using Microsoft.AspNetCore.Mvc;
using CapStoneProject.Entities;
using CapStoneProject.Services;
namespace CapStoneProject.Controllers
{    
    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;      
        public UserController(IUserService service)
        {
            userService = service;
        }

        [HttpPost("create")]   
        public async Task<IActionResult> CreateUser([FromBody] User user)
        {
            var createdUser = await userService.CreateUser(user);
            return StatusCode(StatusCodes.Status201Created, createdUser);
        }

    }
}
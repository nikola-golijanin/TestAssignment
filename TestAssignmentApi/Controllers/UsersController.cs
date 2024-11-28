using Microsoft.AspNetCore.Mvc;
using TestAssignmentApi.Dtos.User;
using TestAssignmentApi.Filters;
using TestAssignmentApi.Services.Users;

namespace TestAssignmentApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [ApiKey]
    public class UsersController : BaseController
    {

        private readonly ILogger<UsersController> _logger;

        private readonly IUserService _userService;

        public UsersController(
            ILogger<UsersController> logger,
            IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        [HttpGet("{id:int}", Name = nameof(GetUserAsync))]
        public async Task<IActionResult> GetUserAsync(int id)
        {
            var result = await _userService.GetUserByIdAsync(id);

            if (result.IsFailure)
            {
                _logger.LogError("Error: {Error}", result.Error.Description);
                return NotFound();
            }

            return Ok(result.Value);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUserAsync([FromBody] CreateNewUserDto newUser)
        {
            var result = await _userService.CreateUserAsync(newUser);
            var user = result.Value;
            return CreatedAtRoute(nameof(GetUserAsync), new { id = user.Id }, user);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteUserAsync(int id)
        {
            var result = await _userService.DeleteUserAsync(id);

            if (result.IsFailure)
            {
                _logger.LogError("{Error}", result.Error.Description);
                return ResolveErrors(error: result.Error);
            }

            return Ok(result.Value);
        }

        [HttpPost("{id:int}/verify-password")]
        public async Task<IActionResult> VerifyUserPasswordAsync(int id, [FromBody] string password)
        {
            var result = await _userService.ValidateUserPasswordAsync(id, password);
            if (result.IsFailure)
            {
                _logger.LogError("{Error}", result.Error.Description);
                return ResolveErrors(error: result.Error);
            }
            return Ok(result.Value);
        }
    }
}

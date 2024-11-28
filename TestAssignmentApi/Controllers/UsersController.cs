using Microsoft.AspNetCore.Mvc;
using TestAssignmentApi.Dtos.User;
using TestAssignmentApi.Services.Users;
using TestAssignmentApi.Utils;

namespace TestAssignmentApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    //[ApiKey]
    public class UsersController : ControllerBase
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

        [HttpGet("{id:int}")]
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
            await _userService.CreateUserAsync(newUser);
            //return CreatedAtAction(nameof(GetUserAsync), new { id = user.Id }, user);
            return Ok();
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


        private IActionResult ResolveErrors(Error error) =>
            error switch
            {
                _ when error.StatusCode == 404 => NotFound(error.Description),
                _ => StatusCode(500)
            };
    }

}

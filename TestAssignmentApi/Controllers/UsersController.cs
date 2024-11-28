using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using TestAssignmentApi.Dtos.Users;
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
            IUserService userService) : base(logger)
        {
            _logger = logger;
            _userService = userService;
        }

        [HttpGet("{id:int}", Name = nameof(GetUserAsync))]
        public async Task<IActionResult> GetUserAsync(int id)
        {
            var result = await _userService.GetUserByIdAsync(id);

            if (result.IsFailure)
                return ResolveErrors(error: result.Error);

            return Ok(result.Value);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUserAsync([FromBody] CreateNewUserDto newUser)
        {
            if (!ModelState.IsValid)
                return ResolveErrors(ModelState);

            var result = await _userService.CreateUserAsync(newUser);
            var user = result.Value;
            return CreatedAtRoute(nameof(GetUserAsync), new { id = user.Id }, user);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteUserAsync(int id)
        {
            var result = await _userService.DeleteUserAsync(id);

            if (result.IsFailure)
                return ResolveErrors(error: result.Error);

            return Ok(result.Value);
        }

        [HttpPost("{id:int}/verify-password")]
        public async Task<IActionResult> VerifyUserPasswordAsync(int id, VerifyPasswordDto passwordDto)
        {
            if (!ModelState.IsValid)
                return ResolveErrors(ModelState);

            var result = await _userService.ValidateUserPasswordAsync(id, passwordDto.Password);

            if (result.IsFailure)
                return ResolveErrors(error: result.Error);

            return Ok(result.Value);
        }

        [HttpPatch("{id:int}")]
        public async Task<IActionResult> UpdateUserAsync(int id, [FromBody] JsonPatchDocument<UserToUpdateDto> patchDoc)
        {
            if (patchDoc is null)
                return BadRequest("patchDoc object sent from client is null.");

            var result = await _userService.UpdateUserAsync(id, patchDoc);
            if (result.IsFailure)
                return ResolveErrors(result.Error);

            return NoContent();
        }
    }
}

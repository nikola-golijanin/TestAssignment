using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
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
        [SwaggerOperation(Summary = "Get a user by ID", Description = "Retrieves a user by their unique ID.")]
        [SwaggerResponse(StatusCodes.Status200OK, "User retrieved successfully", typeof(UserDetailsDto))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "User not found")]
        public async Task<IActionResult> GetUserAsync(int id)
        {
            var result = await _userService.GetUserByIdAsync(id);

            if (result.IsFailure)
                return ResolveErrors(error: result.Error);

            return Ok(result.Value);
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Create a new user", Description = "Creates a new user with the provided details.")]
        [SwaggerResponse(StatusCodes.Status201Created, "User created successfully")]
        [SwaggerResponse(StatusCodes.Status422UnprocessableEntity, "Invalid user data")]
        public async Task<IActionResult> CreateUserAsync([FromBody] CreateNewUserDto newUser)
        {
            if (!ModelState.IsValid)
                return ResolveErrors(ModelState);

            var result = await _userService.CreateUserAsync(newUser);
            var user = result.Value;
            return CreatedAtRoute(nameof(GetUserAsync), new { id = user.Id }, user);
        }

        [HttpDelete("{id:int}")]
        [SwaggerOperation(Summary = "Delete a user", Description = "Deletes a user by their unique ID.")]
        [SwaggerResponse(StatusCodes.Status200OK, "User deleted successfully")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "User not found")]
        public async Task<IActionResult> DeleteUserAsync(int id)
        {
            var result = await _userService.DeleteUserAsync(id);

            if (result.IsFailure)
                return ResolveErrors(error: result.Error);

            return NoContent();
        }

        [HttpPost("{id:int}/verify-password")]
        [SwaggerOperation(Summary = "Verify user password", Description = "Verifies the password of a user by their unique ID.")]
        [SwaggerResponse(StatusCodes.Status200OK, "Password verified successfully. True for valid, false for invalid")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid password data")]
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
        [SwaggerOperation(Summary = "Update a user", Description = "Updates a user with the provided patch document.")]
        [SwaggerResponse(StatusCodes.Status204NoContent, "User updated successfully")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid patch document")]
        public async Task<IActionResult> UpdateUserAsync(int id, [FromBody] JsonPatchDocument<UserToUpdateDto> patchDoc)
        {
            if (patchDoc is null)
                return BadRequest("patchDoc object sent from client is null.");

            var result = await _userService.UpdateUserAsync(id, patchDoc);
            if (result.IsFailure)
                return ResolveErrors(result.Error);

            return NoContent();
        }

        [HttpPut("{id:int}/update-password")]
        [SwaggerOperation(Summary = "Update user password", Description = "Updates the password of a user by their unique ID.")]
        [SwaggerResponse(StatusCodes.Status204NoContent, "Password updated successfully")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid password data")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "User not found")]
        public async Task<IActionResult> UpdateUserPasswordAsync(int id, [FromBody] UpdatePasswordDto updatePasswordDto)
        {
            if (!ModelState.IsValid)
                return ResolveErrors(ModelState);

            var result = await _userService.UpdateUserPasswordAsync(id, updatePasswordDto);

            if (result.IsFailure)
                return ResolveErrors(result.Error);

            return NoContent();
        }
    }
}

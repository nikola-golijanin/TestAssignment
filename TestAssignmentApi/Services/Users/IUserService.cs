using Microsoft.AspNetCore.JsonPatch;
using TestAssignmentApi.Dtos.Users;
using TestAssignmentApi.Models;
using TestAssignmentApi.Utils;

namespace TestAssignmentApi.Services.Users;

public interface IUserService
{
    Task<Result<User>> CreateUserAsync(CreateNewUserDto user);

    Task<Result<UserDetailsDto>> GetUserByIdAsync(int id);

    Task<Result<bool>> DeleteUserAsync(int id);

    Task<Result<User>> UpdateUserAsync(int id, JsonPatchDocument<UserToUpdateDto> patchDoc);

    Task<Result<bool>> ValidateUserPasswordAsync(int id, string password);
}

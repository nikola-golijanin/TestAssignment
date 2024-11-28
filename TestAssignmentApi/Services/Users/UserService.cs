using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using TestAssignmentApi.Data;
using TestAssignmentApi.Dtos.Users;
using TestAssignmentApi.Models;
using TestAssignmentApi.Models.Errors;
using TestAssignmentApi.Utils;

namespace TestAssignmentApi.Services.Users;

public partial class UserService : IUserService
{
    private readonly ApplicationDbContext _context;

    public UserService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<User>> CreateUserAsync(CreateNewUserDto user)
    {
        (string passwordHash, string salt) = PasswordManager.HashPassword(user.Password);

        var newUser = new User
        {
            Username = user.Username,
            PasswordHash = passwordHash,
            Email = user.Email,
            FullName = user.FullName,
            PhoneNumber = user.PhoneNumber,
            Language = user.Language,
            Culture = user.Culture,
            PasswordSalt = salt
        };

        _context.Users.Add(newUser);
        await _context.SaveChangesAsync();
        return Result<User>.Success(newUser);
    }


    public async Task<Result<UserDetailsDto>> GetUserByIdAsync(int id)
    {
        var user = await _context.Users
                                .AsNoTracking()
                                .Where(u => u.Id == id)
                                .Select(u => new UserDetailsDto(u.Id, u.Username, u.Email, u.FullName, u.PhoneNumber, u.Language, u.Culture))
                                .FirstOrDefaultAsync();
        return user is null
            ? Result<UserDetailsDto>.Failure(UserErrors.NotFound)
            : Result<UserDetailsDto>.Success(user);
    }


    public async Task<Result<bool>> DeleteUserAsync(int id)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
        if (user is null) return Result<bool>.Failure(UserErrors.NotFound);

        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
        return Result<bool>.Success(true);
    }


    public async Task<Result<User>> UpdateUserAsync(int id, JsonPatchDocument<UserToUpdateDto> patchDoc)
    {
        var user = await _context.Users
                        .FirstOrDefaultAsync(u => u.Id == id);

        if (user is null)
            return Result<User>.Failure(UserErrors.NotFound);

        var userToPatch = UserToUpdateDto.FromUser(user);
        patchDoc.ApplyTo(userToPatch);
        UserToUpdateDto.MapToUser(userToPatch, user);

        await _context.SaveChangesAsync();
        return Result<User>.Success(user);
    }

    public async Task<Result<bool>> ValidateUserPasswordAsync(int id, string password)
    {
        var user = await _context.Users
                                .AsNoTracking()
                                .FirstOrDefaultAsync(u => u.Id == id);

        if (user is null)
            return Result<bool>.Failure(UserErrors.NotFound);

        var isPasswordValid = PasswordManager.VerifyPassword(password, user.PasswordHash, user.PasswordSalt);
        return isPasswordValid
            ? Result<bool>.Success(true)
            : Result<bool>.Success(false);
    }
}

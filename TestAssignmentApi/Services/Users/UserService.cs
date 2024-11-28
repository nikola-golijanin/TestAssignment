using Microsoft.EntityFrameworkCore;
using TestAssignmentApi.Data;
using TestAssignmentApi.Dtos.User;
using TestAssignmentApi.Models;
using TestAssignmentApi.Models.Errors;
using TestAssignmentApi.Utils;

namespace TestAssignmentApi.Services.Users;

public class UserService : IUserService
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

    public async Task<Result<bool>> DeleteUserAsync(int id)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
        if (user is null) return Result<bool>.Failure(UserErrors.NotFound);

        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
        return Result<bool>.Success(true);
    }

    public async Task<Result<User>> GetUserByIdAsync(int id)
    {
        var user = await _context.Users
                                .AsNoTracking()
                                .FirstOrDefaultAsync(u => u.Id == id);
        return user is null
            ? Result<User>.Failure(UserErrors.NotFound)
            : Result<User>.Success(user);
    }

    public Task UpdateUserAsync(int id, User user)
    {
        throw new NotImplementedException();
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

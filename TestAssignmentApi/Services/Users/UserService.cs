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

    public Task CreateUserAsync(CreateNewUserDto user)
    {
        throw new NotImplementedException();
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

    public Task<bool> ValidateUserPasswordAsync(int id, string password)
    {
        throw new NotImplementedException();
    }
}

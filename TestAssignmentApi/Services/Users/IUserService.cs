﻿using TestAssignmentApi.Dtos.User;
using TestAssignmentApi.Models;
using TestAssignmentApi.Utils;

namespace TestAssignmentApi.Services.Users;

public interface IUserService
{
    Task CreateUserAsync(CreateNewUserDto user);

    Task<Result<User>> GetUserByIdAsync(int id);

    Task<Result<bool>> DeleteUserAsync(int id);

    Task UpdateUserAsync(int id, User user);

    Task<bool> ValidateUserPasswordAsync(int id, string password);
}
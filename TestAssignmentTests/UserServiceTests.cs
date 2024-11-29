using Microsoft.EntityFrameworkCore;
using TestAssignmentApi.Data;
using TestAssignmentApi.Dtos.Users;
using TestAssignmentApi.Models;
using TestAssignmentApi.Models.Errors;
using TestAssignmentApi.Services;
using TestAssignmentApi.Services.Users;

public class UserServiceTests
{
    private readonly UserService _userService;
    private readonly ApplicationDbContext _context;

    public UserServiceTests()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

        _context = new ApplicationDbContext(options);
        _userService = new UserService(_context);
    }

    [Fact]
    public async Task CreateUserAsync_ShouldCreateUser()
    {
        // Arrange
        var newUser = new CreateNewUserDto
        (
            Username: "testuser",
            Password: "password",
            Email: "testuser@example.com",
            FullName: "Test User",
            PhoneNumber: "1234567890",
            Language: "en",
            Culture: "US"
        );

        // Act
        var result = await _userService.CreateUserAsync(newUser);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);
        Assert.Equal(newUser.Username, result.Value.Username);
    }

    [Fact]
    public async Task GetUserByIdAsync_ShouldReturnUser()
    {
        // Arrange
        var user = new User
        {
            Username = "testuser",
            PasswordHash = "passwordHash",
            Email = "testuser@example.com",
            FullName = "Test User",
            PhoneNumber = "1234567890",
            Language = "en",
            Culture = "US",
            PasswordSalt = "salt"
        };
        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        // Act
        var result = await _userService.GetUserByIdAsync(user.Id);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);
        Assert.Equal(user.Username, result.Value.Username);
    }

    [Fact]
    public async Task GetUserByIdAsync_UserNotFound()
    {
        // Arrange
        var unexistingUserId = 1000;

        // Act
        var result = await _userService.GetUserByIdAsync(unexistingUserId);

        // Assert
        // Assert
        Assert.True(result.IsFailure);
        Assert.Equal(UserErrors.NotFound, result.Error);
    }

    [Fact]
    public async Task DeleteUserAsync_ShouldDeleteUser()
    {
        // Arrange
        var user = new User
        {
            Username = "testuser",
            PasswordHash = "passwordHash",
            Email = "testuser@example.com",
            FullName = "Test User",
            PhoneNumber = "1234567890",
            Language = "en",
            Culture = "US",
            PasswordSalt = "salt"
        };
        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        // Act
        var result = await _userService.DeleteUserAsync(user.Id);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Null(await _context.Users.FindAsync(user.Id));
    }

    [Fact]
    public async Task DeleteUserAsync_UserNotFound()
    {
        // Arrange
        var unexistingUserId = 1000;

        // Act
        var result = await _userService.DeleteUserAsync(unexistingUserId);

        // Assert
        Assert.True(result.IsFailure);
        Assert.Equal(UserErrors.NotFound, result.Error);
    }

    [Fact]
    public async Task UpdateUserAsync_ShouldUpdateUser()
    {
        // Arrange
        var user = new User
        {
            Username = "testuser",
            PasswordHash = "passwordHash",
            Email = "testuser@example.com",
            FullName = "Test User",
            PhoneNumber = "1234567890",
            Language = "en",
            Culture = "US",
            PasswordSalt = "salt"
        };
        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        var userToUpdate = new UserToUpdateDto
        (
            Username: "testuser2",
            Email: "testuser2@example.com",
            FullName: "Test User2",
            PhoneNumber: "12345",
            Language: "en",
            Culture: "US"
        );


        // Act
        var result = await _userService.UpdateUserAsync(user.Id, userToUpdate);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(userToUpdate.Email, user.Email);
    }

    [Fact]
    public async Task ValidateUserPasswordAsync_ShouldValidatePassword_PasswordValid()
    {
        var password = "password";
        var (hash, salt) = PasswordManager.HashPassword(password);
        // Arrange
        var user = new User
        {
            Username = "testuser",
            PasswordHash = hash,
            Email = "testuser@example.com",
            FullName = "Test User",
            PhoneNumber = "1234567890",
            Language = "en",
            Culture = "US",
            PasswordSalt = salt
        };
        _context.Users.Add(user);
        await _context.SaveChangesAsync();


        // Act
        var result = await _userService.ValidateUserPasswordAsync(user.Id, password);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.True(result.Value);
    }

    [Fact]
    public async Task ValidateUserPasswordAsync_ShouldValidatePassword_PasswordInvalid()
    {
        var password = "password";
        var wrongPassword = "wrongpassword";
        var (hash, salt) = PasswordManager.HashPassword(password);
        // Arrange
        var user = new User
        {
            Username = "testuser",
            PasswordHash = hash,
            Email = "testuser@example.com",
            FullName = "Test User",
            PhoneNumber = "1234567890",
            Language = "en",
            Culture = "US",
            PasswordSalt = salt
        };
        _context.Users.Add(user);
        await _context.SaveChangesAsync();


        // Act
        var result = await _userService.ValidateUserPasswordAsync(user.Id, wrongPassword);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.False(result.Value);
    }

    [Fact]
    public async Task UpdateUserPasswordAsync_ShouldUpdatePassword()
    {
        var (hash, salt) = PasswordManager.HashPassword("password");

        // Arrange
        var user = new User
        {
            Username = "testuser",
            PasswordHash = hash,
            Email = "testuser@example.com",
            FullName = "Test User",
            PhoneNumber = "1234567890",
            Language = "en",
            Culture = "US",
            PasswordSalt = salt
        };
        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        var updatePasswordDto = new UpdatePasswordDto
        (
            CurrentPassword: "password",
            NewPassword: "newpassword"
        );

        // Act
        var result = await _userService.UpdateUserPasswordAsync(user.Id, updatePasswordDto);

        // Assert
        Assert.True(result.IsSuccess);
    }

    [Fact]
    public async Task UpdateUserPasswordAsync_InvalidPassword()
    {
        var password = "password";
        var wrongPassword = "wrongpassword";
        var (hash, salt) = PasswordManager.HashPassword(password);

        // Arrange
        var user = new User
        {
            Username = "testuser",
            PasswordHash = hash,
            Email = "testuser@example.com",
            FullName = "Test User",
            PhoneNumber = "1234567890",
            Language = "en",
            Culture = "US",
            PasswordSalt = salt
        };
        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        var updatePasswordDto = new UpdatePasswordDto
        (
            CurrentPassword: wrongPassword,
            NewPassword: "newpassword"
        );

        // Act
        var result = await _userService.UpdateUserPasswordAsync(user.Id, updatePasswordDto);

        // Assert
        Assert.True(result.IsFailure);
        Assert.Equal(UserErrors.InvalidPassword, result.Error);
    }
}
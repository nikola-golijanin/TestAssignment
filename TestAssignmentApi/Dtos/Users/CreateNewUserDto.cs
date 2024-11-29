using System.ComponentModel.DataAnnotations;

namespace TestAssignmentApi.Dtos.Users;

public record CreateNewUserDto(
    [Required] string Username,
    [Required] string Password,
    [Required][EmailAddress] string Email,
    [Required] string FullName,
    [Required] string PhoneNumber,
    [Required] string Language,
    [Required] string Culture);

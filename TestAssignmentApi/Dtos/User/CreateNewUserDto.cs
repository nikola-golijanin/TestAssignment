using System.ComponentModel.DataAnnotations;

namespace TestAssignmentApi.Dtos.User;

public record CreateNewUserDto(
    [Required] string Username,
    [Required] string Password,
    [Required][EmailAddress] string Email,
    [Required] string FullName,
    [Required] string MobilePhone,
    [Required] string Language,
    [Required] string Culture)
{
}

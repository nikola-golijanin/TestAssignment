using System.ComponentModel.DataAnnotations;

namespace TestAssignmentApi.Dtos.Users;

public record UpdatePasswordDto(
    [Required] string CurrentPassword,
    [Required] string NewPassword);

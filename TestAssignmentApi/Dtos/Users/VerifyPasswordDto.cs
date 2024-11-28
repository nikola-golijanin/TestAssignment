using System.ComponentModel.DataAnnotations;

namespace TestAssignmentApi.Dtos.Users;

public record VerifyPasswordDto([Required] string Password) { }

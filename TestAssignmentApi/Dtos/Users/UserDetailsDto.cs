namespace TestAssignmentApi.Dtos.Users;

public record UserDetailsDto(
        int Id,
    string Username,
    string Email,
    string FullName,
    string PhoneNumber,
    string Language,
    string Culture);

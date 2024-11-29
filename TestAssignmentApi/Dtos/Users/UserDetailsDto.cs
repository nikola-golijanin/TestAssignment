using TestAssignmentApi.Models;

namespace TestAssignmentApi.Dtos.Users;

public record UserDetailsDto(
        int Id,
    string Username,
    string Email,
    string FullName,
    string PhoneNumber,
    string Language,
    string Culture)
{
    public static UserDetailsDto FromUser(User user)
    {
        return new UserDetailsDto(
            user.Id,
            user.Username,
            user.Email,
            user.FullName,
            user.PhoneNumber,
            user.Language,
            user.Culture
        );
    }
}

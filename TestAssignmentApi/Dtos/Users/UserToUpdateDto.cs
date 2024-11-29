using TestAssignmentApi.Models;

namespace TestAssignmentApi.Dtos.Users;

public record UserToUpdateDto(
    string Username,
    string Email,
    string FullName,
    string PhoneNumber,
    string Language,
    string Culture)
{
    public static UserToUpdateDto FromUser(User user)
    {
        return new UserToUpdateDto(
            Username: user.Username,
            Email: user.Email,
            FullName: user.FullName,
            PhoneNumber: user.PhoneNumber,
            Language: user.Language,
            Culture: user.Culture
        );
    }

    public static void MapToUser(UserToUpdateDto userToPatch, User user)
    {
        user.Username = userToPatch.Username;
        user.Email = userToPatch.Email;
        user.FullName = userToPatch.FullName;
        user.PhoneNumber = userToPatch.PhoneNumber;
        user.Language = userToPatch.Language;
        user.Culture = userToPatch.Culture;
    }
}
using System.ComponentModel.DataAnnotations;
using TestAssignmentApi.Models;

namespace TestAssignmentApi.Dtos.Users;

public record UserToUpdateDto(
    [Required] string Username,
    [EmailAddress] string Email,
    [Required] string FullName,
    [Required] string PhoneNumber,
    [Required] string Language,
    [Required] string Culture)
{
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

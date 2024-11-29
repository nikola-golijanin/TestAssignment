using TestAssignmentApi.Utils;

namespace TestAssignmentApi.Models.Errors;

public static class UserErrors
{
    public static readonly Error NotFound = new(
        Code: "User.NotFound",
        Description: "User does not exist", 404);

    public static readonly Error InvalidPassword = new(
        Code: "User.InvalidPassword",
        Description: "Users current password is invalid", 400);
}

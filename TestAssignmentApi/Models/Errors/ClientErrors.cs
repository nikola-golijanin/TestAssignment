
using TestAssignmentApi.Utils;

namespace TestAssignmentApi.Models.Errors;

public class ClientErrors
{
    public static readonly Error NotFound = new(
        Code: "Client.NotFound",
        Description: "Client does not exist", 404);

}

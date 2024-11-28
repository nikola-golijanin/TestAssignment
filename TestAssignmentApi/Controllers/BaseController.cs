using Microsoft.AspNetCore.Mvc;
using TestAssignmentApi.Utils;

namespace TestAssignmentApi.Controllers;

public class BaseController : ControllerBase
{
    protected IActionResult ResolveErrors(Error error) =>
        error switch
        {
            _ when error.StatusCode == 404 => NotFound(error.Description),
            _ when error.StatusCode == 400 => BadRequest(error.Description),
            _ => StatusCode(500)
        };
}

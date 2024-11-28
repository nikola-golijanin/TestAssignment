using Microsoft.AspNetCore.Mvc;
using TestAssignmentApi.Utils;

namespace TestAssignmentApi.Controllers;

public class BaseController : ControllerBase
{
    private readonly ILogger<BaseController> _logger;

    public BaseController(ILogger<BaseController> logger)
    {
        _logger = logger;
    }

    protected IActionResult ResolveErrors(Error error)
    {
        // Log the error
        _logger.LogError("{Error}", error.Description);

        return error switch
        {
            _ when error.StatusCode == 404 => NotFound(error.Description),
            _ when error.StatusCode == 400 => BadRequest(error.Description),
            _ => StatusCode(500)
        };
    }
}

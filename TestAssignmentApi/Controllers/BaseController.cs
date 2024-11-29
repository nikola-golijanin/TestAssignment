using Microsoft.AspNetCore.Mvc;
using System.Net;
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
        _logger.LogError("{Error}", error.Description);

        return error switch
        {
            _ when error.StatusCode == 404 => NotFound(ToProblemDetails(error)),
            _ when error.StatusCode == 400 => BadRequest(ToProblemDetails(error)),
            _ => StatusCode(((int)HttpStatusCode.InternalServerError))
        };

        static ProblemDetails ToProblemDetails(Error error) =>
            new()
            {
                Status = error.StatusCode,
                Type = error.Code,
                Title = error.Code,
                Detail = error.Description
            };
    }
}

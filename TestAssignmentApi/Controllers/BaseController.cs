using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
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
            _ when error.StatusCode == 404 => NotFound(error.Description),
            _ when error.StatusCode == 400 => BadRequest(error.Description),
            _ => StatusCode(500)
        };
    }

    protected IActionResult ResolveErrors(ModelStateDictionary modelState)
    {
        foreach (var error in modelState.Values.SelectMany(value => value.Errors))
            _logger.LogError("{Error}", error.ErrorMessage);

        return UnprocessableEntity(ModelState);
    }
}

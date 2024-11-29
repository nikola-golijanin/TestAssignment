using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace TestAssignmentApi.Attributes;

public class ValidateModelAttribute : ActionFilterAttribute
{
    public override void OnResultExecuting(ResultExecutingContext executionContext)
    {
        var _logger = executionContext.HttpContext.RequestServices.GetRequiredService<ILogger<ValidateModelAttribute>>();
        List<string>? errorMessages = [];
        if (!executionContext.ModelState.IsValid)
        {
            foreach (var error in executionContext.ModelState.Values.SelectMany(value => value.Errors))
                errorMessages.Add(error.ErrorMessage);

            _logger.LogError("{Errors}", errorMessages);
            executionContext.Result = new UnprocessableEntityObjectResult(executionContext.ModelState);
        }
    }
}

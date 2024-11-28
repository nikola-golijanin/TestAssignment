using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using TestAssignmentApi.Data;

namespace TestAssignmentApi.Filters;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class ApiKeyAttribute : Attribute, IAuthorizationFilter
{
    private const string ApiKeyHeaderName = "X-API-Key";

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        if (!IsApiKeyValid(context.HttpContext))
        {
            context.Result = new UnauthorizedResult();
        }
    }

    private static bool IsApiKeyValid(HttpContext context)
    {
        string? apiKey = context.Request.Headers[ApiKeyHeaderName];
        if (string.IsNullOrWhiteSpace(apiKey)) return false;

        var dbContext = context.RequestServices
                        .GetRequiredService<ApplicationDbContext>();

        var apiKeyGuid = Guid.Parse(apiKey);
        return dbContext.Clients
            .Any(c => c.ApiKey == apiKeyGuid);
    }
}

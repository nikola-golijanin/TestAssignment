namespace TestAssignmentApi.Middlewares;

public class RequestLoggingMiddleware
{
    private const string ApiKeyHeaderName = "X-API-Key";
    private readonly RequestDelegate _next;


    private readonly ILogger<RequestLoggingMiddleware> _logger;
    public RequestLoggingMiddleware(
        ILogger<RequestLoggingMiddleware> logger,
        RequestDelegate next
       )
    {
        _logger = logger;
        _next = next;
    }
    public async Task Invoke(HttpContext context)
    {
        var request = context.Request;
        var callerIp = context.Connection.RemoteIpAddress?.ToString();
        var hostName = context.Request.Host.Value;
        var requestParams = string.Join(", ", request.Query.Select(q => $"{q.Key}={q.Value}"));
        string? apiKey = context.Request.Headers[ApiKeyHeaderName];

        _logger.LogInformation("Incoming request: {Method} {Path}, callerIp: {CallerIp}, hostName: {HostName}, requestParams: {RequestParams}, clientApiKey: {ApiKey}",
            request.Method,
            request.Path,
            callerIp,
            hostName,
            requestParams,
            apiKey ?? string.Empty);

        await _next.Invoke(context);
    }

}

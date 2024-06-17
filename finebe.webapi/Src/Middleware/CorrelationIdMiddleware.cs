using finebe.webapi.Src.Enums;
using Microsoft.Extensions.Primitives;
using Serilog.Context;

namespace finebe.webapi;

public class CorrelationIdMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger _logger;
    public CorrelationIdMiddleware(
        RequestDelegate next,
        ILoggerFactory loggerFactory)
    {
        _next = next ?? throw new ArgumentNullException(nameof(next));
        _logger = loggerFactory.CreateLogger<CorrelationIdMiddleware>();
    }
    public async Task Invoke(HttpContext httpContext)
    {
        string correlationId = null;
        if (httpContext.Request.Headers.TryGetValue(SettingsEnum.CorrelationIdHeaderKey, out StringValues correlationIds))
        {
            correlationId = correlationIds.FirstOrDefault(k => k.Equals(SettingsEnum.CorrelationIdHeaderKey));
            _logger.LogInformation($"CorrelationId from Request Header: {correlationId}");
        }
        else
        {
            correlationId = Guid.NewGuid().ToString();
            httpContext.Request.Headers[SettingsEnum.CorrelationIdHeaderKey] = correlationId;

            // Override the Serilog creation
            LogContext.PushProperty(SettingsEnum.CorrelationIdHeaderKey, correlationId);

            // TODO TLB: Add to some sort of FinebeDiagnosticContext

            _logger.LogInformation($"Generated CorrelationId: {correlationId}");
        }
        httpContext.Response.OnStarting(() =>
        {
            if (!httpContext.Response.Headers.TryGetValue(SettingsEnum.CorrelationIdHeaderKey, out correlationIds))
            httpContext.Response.Headers[SettingsEnum.CorrelationIdHeaderKey] = correlationId;
            return Task.CompletedTask;
        });

        await _next.Invoke(httpContext);
    }
}

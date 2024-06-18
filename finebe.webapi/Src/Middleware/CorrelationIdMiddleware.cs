using finebe.webapi.Src.Interfaces;
using Microsoft.Extensions.Primitives;
using Serilog.Context;

namespace finebe.webapi.Src.Middleware;

public class CorrelationIdMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger _logger;

    public CorrelationIdMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
    {
        _next = next ?? throw new ArgumentNullException(nameof(next));
        _logger = loggerFactory.CreateLogger<CorrelationIdMiddleware>();
    }

    public async Task Invoke(
        HttpContext httpContext, 
        IConfiguration configuration, 
        IFinebeDiagnosticContext finebeDiagnosticContext)
    {
        string correlationIdKey = configuration.GetValue<string>("Settings:CorrelationIdKey");
        if (string.IsNullOrEmpty(correlationIdKey))
        {
            throw new ArgumentException("Correlation ID Key cannot be null or empty.", nameof(correlationIdKey));
        }

        string correlationId = null;

        if (httpContext.Request.Headers.TryGetValue(correlationIdKey, out StringValues requestCorrelationIds))
        {
            correlationId = requestCorrelationIds.FirstOrDefault();
            _logger.LogInformation("CorrelationId from Request Header: {CorrelationId}", correlationId);
        }
        else
        {
            if (httpContext.Response.Headers.TryGetValue(correlationIdKey, out StringValues responseCorrelationIds))
            {
                correlationId = responseCorrelationIds.FirstOrDefault();
                _logger.LogInformation("CorrelationId from Response Header: {CorrelationId}", correlationId);
            }
            else
            {
                correlationId = Guid.NewGuid().ToString();
                _logger.LogInformation("CorrelationId generated: {CorrelationId}", correlationId);
            }

            LogContext.PushProperty(correlationIdKey, correlationId);
            httpContext.Request.Headers[correlationIdKey] = correlationId;
            _logger.LogInformation("Request header CorrelationId: {CorrelationId}", httpContext.Request.Headers[correlationIdKey]);
        }

        finebeDiagnosticContext.CorrelationId = correlationId;
        _logger.LogInformation("FinebeDiagnosticContext CorrelationId: {CorrelationId}", finebeDiagnosticContext.CorrelationId);

        httpContext.Response.OnStarting(() =>
        {
            httpContext.Response.Headers[correlationIdKey] = correlationId;
            _logger.LogInformation("Response header CorrelationId: {CorrelationId}", httpContext.Response.Headers[correlationIdKey]);

            return Task.CompletedTask;
        });

        await _next(httpContext);
    }
}

using System.Security.Claims;
using finebe.webapi.Src.Interfaces;

namespace finebe.webapi.Src.Services;

public class AuthenticatedUserService : IAuthenticatedUserService
{
    private readonly ILogger<AuthenticatedUserService> _logger;
    private readonly IHttpContextAccessor _httpContextAccessor;
    public Guid? Guid { get; }
    public string Email { get; }

    public AuthenticatedUserService(ILogger<AuthenticatedUserService> logger, IHttpContextAccessor httpContextAccessor)
    {
        _logger = logger;
        _httpContextAccessor = httpContextAccessor;

        _logger.LogInformation("Init");
        
        var user = _httpContextAccessor.HttpContext?.User;
        
        var nameIdentifier = user?.FindFirstValue(ClaimTypes.NameIdentifier);
        if (System.Guid.TryParse(nameIdentifier, out var guid))
        {
            Guid = guid;
        }
        else
        {
            Guid = null;
        }

        Email = user?.FindFirstValue(ClaimTypes.Name);

        _logger.LogInformation($"Results [Guid: {Guid}], [Email: {Email}]");
    }

    public bool IsAuthenticated()
    {
        _logger.LogInformation("Init");
        return _httpContextAccessor.HttpContext?.User?.Identity?.IsAuthenticated ?? false;
    }
}

using System.Security.Claims;
using finebe.webapi.Src.Interfaces;

namespace finebe.webapi.Src.Services;

public class AuthenticatedUserService : IAuthenticatedUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    public Guid? Guid { get; }
    public string Email { get; }

    public AuthenticatedUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
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
    }

    public bool IsAuthenticated()
    {
        return _httpContextAccessor.HttpContext?.User?.Identity?.IsAuthenticated ?? false;
    }
}

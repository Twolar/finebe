using System.Security.Claims;
using finebe.webapi.Src.Interfaces;

namespace finebe.webapi.Src.Services;

public class AuthenticatedUserService : IAuthenticatedUserService
{
    public AuthenticatedUserService(IHttpContextAccessor httpContextAccessor)
    {
        Uid = httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
    }

    public string Uid { get; }
    public string UUsername { get; }
}

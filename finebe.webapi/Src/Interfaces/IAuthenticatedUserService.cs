namespace finebe.webapi.Src.Interfaces;

public interface IAuthenticatedUserService
{
    Guid? Guid { get; }
    public string Email { get; }
    public bool IsAuthenticated();
}

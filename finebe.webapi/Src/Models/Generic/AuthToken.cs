namespace finebe.webapi.Src.Models.Generic;

public class AuthToken
{
    public string Token { get; set; }
    public DateTime Expiration { get; set; }
    public DateTime IssuedAt { get; set; }
    public Guid UserId { get; set; }
    public string UserName { get; set; }
    public List<string> Roles { get; set; }
    public Dictionary<string, string> Claims { get; set; }
    public string RefreshToken { get; set; }

    public AuthToken()
    {
        Roles = new List<string>();
        Claims = new Dictionary<string, string>();
    }
}

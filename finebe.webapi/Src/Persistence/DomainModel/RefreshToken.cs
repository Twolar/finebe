using System.Text.Json.Serialization;

namespace finebe.webapi.Src.Persistence.DomainModel;

public class RefreshToken : BaseEntity
{
    public string Token { get; set; }
    public Guid UserId { get; set; }
    public DateTime Expiration { get; set; }
    public bool IsRevoked { get; set; }

    [JsonIgnore]
    public ApplicationUser ApplicationUser { get; set; }
}
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;

namespace finebe.webapi.Src.Persistence.DomainModel;

public class ApplicationUser : IdentityUser
{
    public string Name { get; set; }
    public string Avatar { get; set; }

    // Navigation property for the relationship
    [JsonIgnore]
    public ICollection<Trip> Trips { get; set; }
}

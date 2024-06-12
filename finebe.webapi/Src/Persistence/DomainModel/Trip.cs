using System.Text.Json.Serialization;

namespace finebe.webapi.Src.Persistence.DomainModel;

public class Trip : BaseEntity
{
    public string Location { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    // Foreign key to ApplicationUser
    public Guid ApplicationUserId { get; set; }

    // Navigation property
    [JsonIgnore]
    public ApplicationUser ApplicationUser { get; set; }
}

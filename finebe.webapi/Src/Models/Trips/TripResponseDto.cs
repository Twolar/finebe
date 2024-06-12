using finebe.webapi.Src.Persistence.DomainModel;

namespace finebe.webapi.Src.Models.Trips;

public class TripResponseDto : BaseEntity
{
    public string Location { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    public string ApplicationUserId { get; set; }
}

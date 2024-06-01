namespace finebe.webapi;

public class TripModel : BaseEntity
{
    public string Location { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}

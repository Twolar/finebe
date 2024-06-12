using System.ComponentModel.DataAnnotations;

namespace finebe.webapi.Src.Models.Trips;

public class TripRequestDto
{
    public string Location { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    [Required]
    public string ApplicationUserId { get; set; }
}

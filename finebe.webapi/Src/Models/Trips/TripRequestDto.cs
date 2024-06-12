using System.ComponentModel.DataAnnotations;

namespace finebe.webapi.Src.Models.Trips;

public class TripRequestDto
{
    [Required]
    public string Location { get; set; }

    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    [Required] // Nullable is neccessary for Required to work
    public Guid? ApplicationUserId { get; set; }
}

using System.ComponentModel.DataAnnotations.Schema;

namespace finebe_api.Models;

public class Order
{
    public int Id { get; set; }
    public decimal Amount { get; set; }
    public Guid UserId { get; set; } // Replace CustomerId with UserId

    [ForeignKey("UserId")]
    public ApplicationUser User { get; set; } // Replace Customer with ApplicationUser
}

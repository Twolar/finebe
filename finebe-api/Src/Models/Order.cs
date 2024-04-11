using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace finebe_api.Models;

[PrimaryKey("Id")]
public class Order
{
    public Guid Id { get; set; }
    public decimal Amount { get; set; }
    public Guid UserId { get; set; } // Replace CustomerId with UserId

    [ForeignKey("UserId")]
    public ApplicationUser User { get; set; } // Replace Customer with ApplicationUser
}

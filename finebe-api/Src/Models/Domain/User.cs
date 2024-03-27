using finebe_api.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace finebe_api.Models.Domain;

public class User : IdentityUser<Guid>, IBaseEntity
{
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public bool IsActive { get; set; }
}

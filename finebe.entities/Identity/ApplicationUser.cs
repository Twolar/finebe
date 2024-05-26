using Microsoft.AspNetCore.Identity;

namespace finebe.entities.Identity;

public class ApplicationUser : IdentityUser
{
    public string Name { get; set; }
    public string Avatar { get; set; }
}

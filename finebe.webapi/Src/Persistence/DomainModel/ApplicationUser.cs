﻿using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;

namespace finebe.webapi.Src.Persistence.DomainModel;

public class ApplicationUser : IdentityUser<Guid>
{
    public string Name { get; set; }
    public string Avatar { get; set; }

    // Navigation property for the relationship
    [JsonIgnore]
    public ICollection<Trip> Trips { get; set; }
     [JsonIgnore]
    public ICollection<RefreshToken> RefreshTokens { get; set; }
}
